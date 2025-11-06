using DataBaseManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataBaseManager.Dtos;
using System.Globalization;
using Humanizer;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Net;
using DataBaseManager.Dtos.User;
using DataBaseManager.Models.AuthModels;
using DataBaseManager.Extensions;
using DataBaseManager.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataBaseManager.Controllers.AuthController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public partial class AuthController : AppControllerBase
    {
        [AutoInject] private readonly IOptionsMonitor<BearerTokenOptions> bearerTokenOptions = default!;
        [AutoInject] private readonly IUserStore<User> _userStore = default!;
        [AutoInject] private readonly SignInManager<User> _signInManager = default!;
        [AutoInject] private readonly ITokenService _tokenService = default!;
        [AutoInject] private readonly UserManager<User> _userManager = default!;
        [AutoInject] private readonly IUserService _userService = default!;
        [AutoInject] private readonly SmsService _smsService = default!;


        [HttpPost]
        public async Task<ActionResult<ApiResponse>> SignUp([FromBody]SignUpRequestDto request , CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByPhoneNumber(request.PhoneNumber!) ;
            if (existingUser != null)
                return ApiResponse.Fail($"This phone number :{request.PhoneNumber} is already taken.", HttpStatusCode.Conflict);

            var Dto = new UserCreateRequestDto
            {
                PhoneNumber = request.PhoneNumber,
                UserName = request.PhoneNumber
            };

            var addedUser = await _userService.CreateAsync(Dto);
            await SendConfirmPhoneToken(addedUser.Body!, cancellationToken);

            return ApiResponse.Success("Token sent via sms..." , HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> SignIn([FromBody]SignInRequestDto request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByPhoneNumber(request.PhoneNumber!);
            if (existingUser == null)
                return ApiResponse.Fail("User does not exist.", HttpStatusCode.NotFound);

            existingUser.LockoutEnabled = true;

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await SendConfirmPhoneToken(existingUser, cancellationToken);

            return ApiResponse.Success("Token sent via sms...", HttpStatusCode.OK);

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> SignOut([FromBody] SignOutRequestDto request , CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return ApiResponse.Fail("RefreshToken is required.", HttpStatusCode.BadRequest);

            var user = await _userManager.Users
            .Include(u => u.Sessions)!
            .ThenInclude(s => s.Token)
            .FirstOrDefaultAsync(u => u.Sessions!.Any(s => s.Token!.RefreshToken == request.RefreshToken));

            if (user == null)
            {
                return ApiResponse.Fail("Token invalid!", HttpStatusCode.BadRequest);
            }

            var session = user.Sessions!.FirstOrDefault(s => s.Token!.RefreshToken == request.RefreshToken); // handle token removal !
            if (session != null)
            {
                user.Sessions!.Remove(session);
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return ApiResponse.Fail("Failed to update user sessions." , HttpStatusCode.InternalServerError);
            }

            return ApiResponse.Success("Logged out successfully." , HttpStatusCode.OK);
        }
        
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Token>>> Refresh([FromBody] RefreshRequestDto request, CancellationToken cancellationToken) // fix the removing part! 
        {
            if (request?.RefreshToken == null)
                return BadRequest("RefreshToken is required.");
            var user = await _userManager.Users
            .Include(u => u.Sessions)!
            .ThenInclude(s => s.Token)
            .FirstOrDefaultAsync(u => u.Sessions!.Any(s => s.Token!.RefreshToken == request.RefreshToken));

            if (user == null)
                return ApiResponse<Token>.Fail("Invalid refresh token." , HttpStatusCode.Unauthorized);

            var session = user.Sessions!.FirstOrDefault(s => s.Token!.RefreshToken == request.RefreshToken);
            if (session == null)
                return ApiResponse<Token>.Fail("Invalid refresh token.", HttpStatusCode.Unauthorized);
            var Token = await _tokenService.GenerateTokensAsync(user);
            session.Token = Token;
            _appDbContext.Entry(Token).State = EntityState.Modified;
            _appDbContext.Entry(session).State = EntityState.Modified;


            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return ApiResponse<Token>.Success("Failed to update user sessions.", HttpStatusCode.OK);
            }
            return ApiResponse<Token>.Success("Tokens Refreshed Syccessfully" , HttpStatusCode.OK , session.Token);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> ConfirmSignUpPhone([FromBody] ConfirmPhoneRequestDto request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByPhoneNumber(request.PhoneNumber!);
            if(user == null)
            
                return ApiResponse.Fail("User does not exist." , HttpStatusCode.NotFound);
            

            if (await _userManager.IsLockedOutAsync(user))
            {
                var waitTime = (user.LockoutEnd!.Value - DateTimeOffset.UtcNow).Humanize();
                return ApiResponse.Fail($"User is locked out. Try again in {waitTime}.", HttpStatusCode.BadRequest);
            }

            var tokenIsValid = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultPhoneProvider, FormattableString.Invariant($"VerifyPhoneNumber:{request.PhoneNumber},{user.PhoneNumberTokenRequestedOn?.ToUniversalTime()}"), request.Token!);

            if (!tokenIsValid)
            {
                await _userManager.AccessFailedAsync(user);
                return ApiResponse.Fail("Invalid token.", HttpStatusCode.BadRequest);
            }

            await ((IUserPhoneNumberStore<User>)_userStore).SetPhoneNumberConfirmedAsync(user, true, cancellationToken);
            await _userManager.ResetAccessFailedCountAsync(user);

            user.PhoneNumberTokenRequestedOn = null;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return ApiResponse.Fail("Concurrency Error, Something conflicted...", HttpStatusCode.Conflict);

            return ApiResponse.Success("Phone Confirmed Successfully", HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Token>>> ConfirmSignInPhone([FromBody] ConfirmPhoneRequestDto request, CancellationToken cancellationToken)
        {
            var user = await _appIdentityDbContext.Users.Include(op => op.Sessions)!.ThenInclude(op => op.Token).FirstAsync(op => op.PhoneNumber == request.PhoneNumber);
            if (user == null)
                return ApiResponse<Token>.Fail("User does not exist.", HttpStatusCode.NotFound);

            if (!user.PhoneNumberConfirmed)
                return ApiResponse<Token>.Fail("Phone number is not confirmed. Please complete sign up first.", HttpStatusCode.BadRequest);
            if (await _userManager.IsLockedOutAsync(user))
                return ApiResponse<Token>.Fail($"User is locked out. Try again in {(user.LockoutEnd!.Value - DateTimeOffset.UtcNow).Humanize()}.", HttpStatusCode.BadRequest);


            var tokenIsValid = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultPhoneProvider, FormattableString.Invariant($"VerifyPhoneNumber:{request.PhoneNumber},{user.PhoneNumberTokenRequestedOn?.ToUniversalTime()}"), request.Token!);
            if (!tokenIsValid)
            {
                await _userManager.AccessFailedAsync(user);
                return ApiResponse<Token>.Fail("Invalid token.", HttpStatusCode.BadRequest);
            }

            await _userManager.ResetAccessFailedCountAsync(user);
            user.PhoneNumberTokenRequestedOn = null;

            user.Sessions?.RemoveAll(s => s.IsExpired()); // fix here for removing the tokens as well !
            var session = CreateUserSession(request.DeviceInfo);
            var Token = await _tokenService.GenerateTokensAsync(user);
            Token.Id = session.SessionUniqueId;
            session.Token = Token;
            _appDbContext.Entry(session).State = EntityState.Added;
            _appDbContext.Entry(Token).State = EntityState.Added;

            user.Sessions?.Add(session);

            //var result = await _userManager.UpdateAsync(user);
            //if (!result.Succeeded)
            //    return ApiResponse<Token>.Fail("Concurrency Error, Something conflicted...", HttpStatusCode.Conflict);

            var result = await _appDbContext.SaveChangesAsync();

            return ApiResponse<Token>.Success("Session Created Successfully", HttpStatusCode.OK , session.Token);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> SendConfirmPhoneToken([FromBody] SendPhoneTokenRequestDto request , CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByPhoneNumber(request.PhoneNumber!);
          if (user == null)
                return ApiResponse.Fail("User does not exist.", HttpStatusCode.NotFound);

            if (await _userManager.IsPhoneNumberConfirmedAsync(user))
                return ApiResponse.Fail("Your phone number is already confirmed.", HttpStatusCode.BadRequest);

            await SendConfirmPhoneToken(user, cancellationToken);

            return ApiResponse.Success("Token sent via sms...", HttpStatusCode.OK);
        }




        private async Task SendConfirmPhoneToken(User user, CancellationToken cancellationToken)
        {
            var resendDelay = (DateTimeOffset.UtcNow - user.PhoneNumberTokenRequestedOn) - _appSettings.Identity.PhoneNumberTokenRequestResendDelay;
            if (resendDelay < TimeSpan.Zero)
                throw new TooManyRequestsExceptions($"You have already requested the confirmation sms. Try again in {resendDelay.Value.Humanize(culture: CultureInfo.CurrentUICulture)}") ;
            
            user.PhoneNumberTokenRequestedOn = DateTimeOffset.UtcNow;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded is false)
            throw new ResourceValidationException(result.Errors.Select(e => (e.Code, e.Description)).ToString()!);

            var phoneNumber = user.PhoneNumber!;
            var token = await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultPhoneProvider, FormattableString.Invariant($"VerifyPhoneNumber:{phoneNumber},{user.PhoneNumberTokenRequestedOn?.ToUniversalTime()}"));
            
            await _smsService.SendSms($"{token} is your confirm phone number token in Binesh.", phoneNumber, cancellationToken);

        }
        private UserSession CreateUserSession(string? device)
        {
            var userSession = new UserSession
            {
                SessionUniqueId = Guid.NewGuid(),
                // Relying on Cloudflare cdn to retrieve address.
                // https://developers.cloudflare.com/rules/transform/managed-transforms/reference/#add-visitor-location-headers
                Address = $"{Request.Headers["cf-ipcountry"]}, {Request.Headers["cf-ipcity"]}",
                Device = device ?? "Unknown device",
                IP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                StartedOn = DateTimeOffset.UtcNow
            };
            return userSession;
        }
    }
}