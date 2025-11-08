using BineshSoloution.DbContexts;
using BineshSoloution.Models;
using BineshSoloution.Models.AuthModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace BineshSoloution.Services;

public partial class TokenService : ITokenService
{
    [AutoInject] private readonly DataProtection _DataProtection = default!;
    [AutoInject] private readonly IdentitySettings _IdentitySettings = default!;
    [AutoInject] private readonly UserManager<User> _userManager = default!;
    [AutoInject] private readonly ApplicationDbContext _context = default!;
    [AutoInject] private readonly X509Certificate2 _certificate = default!;
    [AutoInject] private readonly IWebHostEnvironment _env = default!;

    public TokenService()
    {
        var certificatePath = _DataProtection.DataProtectionCertificatePath ?? throw new Exception(); // fix the exception type 
        var certificatePassword = _DataProtection.DataProtectionCertificatePassword ?? throw new Exception();

        _certificate = new X509Certificate2(
            certificatePath,
            certificatePassword,
            OperatingSystem.IsWindows() ? X509KeyStorageFlags.EphemeralKeySet : X509KeyStorageFlags.DefaultKeySet);

        if (_certificate.Thumbprint == "55140A8C935AB520294922071E5781E6946CD60606" && !_env.IsDevelopment())
        {
            throw new InvalidOperationException("The default test certificate is still in use.");
        }
    }

    public async Task<Token> GenerateTokensAsync(User user)
    {
        var accessToken = await GenerateAccessTokenAsync(user);
        var refreshToken = GenerateRefreshToken();

        var TokenEntity = new Token
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            UserId = user.Id,
            Expires = DateTime.UtcNow.AddDays(_IdentitySettings.BearerTokenExpiration.TotalDays),
            Created = DateTime.UtcNow
        };

        return TokenEntity;
    }

    private async Task<string> GenerateAccessTokenAsync(User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var signingCredentials = new SigningCredentials(new X509SecurityKey(_certificate), SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            issuer: _IdentitySettings.Issuer,
            audience: _IdentitySettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_IdentitySettings.BearerTokenExpiration.TotalDays),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var signingCredentials = new SigningCredentials(new X509SecurityKey(_certificate), SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            issuer: _IdentitySettings.Issuer,
            audience: _IdentitySettings.Audience,
            expires: DateTime.UtcNow.AddDays(_IdentitySettings.RefreshTokenExpiration.TotalDays),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
