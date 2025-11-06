using DataBaseManager.Models;
using Microsoft.AspNetCore.Identity;
using DataBaseManager.Dtos.User;
using DataBaseManager.Dtos;
using System.Security.Claims;
using DataBaseManager.Models.AuthModels;

namespace DataBaseManager.Services;

public interface IUserService
{
    Task<ApiResponse<User>> GetByIdAsync(Guid id);
    Task<ApiResponse<List<User>>> GetAllAsync();
    Task<ApiResponse<User>> CreateAsync(UserCreateRequestDto request);
    Task<ApiResponse> UpdateAsync(Guid id, UserUpdateRequestDto request);
    Task<ApiResponse> DeleteAsync(Guid id);
}
