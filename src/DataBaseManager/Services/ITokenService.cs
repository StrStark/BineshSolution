using BineshSoloution.Models;
using BineshSoloution.Models.AuthModels;
using Microsoft.AspNetCore.Authentication;

namespace BineshSoloution.Services;
public interface ITokenService
{
    Task<Token> GenerateTokensAsync(User user);
}
