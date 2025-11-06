using DataBaseManager.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos.User;
using Shared.Dtos;
using System.Security.Claims;

namespace DataBaseManager.Services;

public interface IUserService
{
    Task<ApiResponse<User>> GetByIdAsync(Guid id);
    Task<ApiResponse<List<User>>> GetAllAsync();
    Task<ApiResponse<User>> CreateAsync(UserCreateRequestDto request);
    Task<ApiResponse> UpdateAsync(Guid id, UserUpdateRequestDto request);
    Task<ApiResponse> DeleteAsync(Guid id);
}
