using DataBaseManager.Models;
using Microsoft.AspNetCore.Authentication;

namespace DataBaseManager.Services;
public interface ITokenService
{
    Task<Token> GenerateTokensAsync(User user);
}
