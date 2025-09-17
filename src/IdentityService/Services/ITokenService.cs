using IdentityService.Models;
using Microsoft.AspNetCore.Authentication;

namespace IdentityService.Services;
public interface ITokenService
{
    Task<Token> GenerateTokensAsync(User user);
}
