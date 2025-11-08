using BineshSoloution.Models;
using Microsoft.AspNetCore.Identity;
using BineshSoloution.Dtos.User;
using BineshSoloution.Dtos;
using System.Security.Claims;
using BineshSoloution.Models.AuthModels;

namespace BineshSoloution.Services;

public interface IUserService
{
    Task<ApiResponse<User>> GetByIdAsync(Guid id);
    Task<ApiResponse<List<User>>> GetAllAsync();
    Task<ApiResponse<User>> CreateAsync(UserCreateRequestDto request);
    Task<ApiResponse> UpdateAsync(Guid id, UserUpdateRequestDto request);
    Task<ApiResponse> DeleteAsync(Guid id);
}
