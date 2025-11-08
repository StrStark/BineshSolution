using AutoMapper;
using BineshSoloution.Models;
using BineshSoloution.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BineshSoloution.Dtos;
using BineshSoloution.Dtos.User;
using System.Net;
using System.Security.Claims;

namespace BineshSoloution.Services;

public partial class UserService : IUserService
{
    [AutoInject] private readonly IMapper _mapper =default!;
    [AutoInject] private readonly UserManager<User> _userManager = default!;


    public async Task<ApiResponse<User>> GetByIdAsync(Guid id)
    {
        var user = await _userManager.Users
            .Include(u => u.Sessions)!
            .ThenInclude(s => s.Token)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return ApiResponse<User>.Fail("User not found"  ,HttpStatusCode.NotFound , user);

        return ApiResponse<User>.Success("User retrieved", HttpStatusCode.OK, user);
    }
    public async Task<ApiResponse<List<User>>> GetAllAsync()
    {
        var users = await _userManager.Users
            .Include(u => u.Sessions)!
            .ThenInclude(s => s.Token)
            .ToListAsync();

        return ApiResponse<List<User>>.Success("Users retrieved", HttpStatusCode.OK , users);
    }
    public async Task<ApiResponse<User>> CreateAsync(UserCreateRequestDto request)
    {
        var user = _mapper.Map<User>(request);

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
            return ApiResponse<User>.Fail(string.Join(", ", result.Errors.Select(e => e.Description)), HttpStatusCode.BadRequest);

        return ApiResponse<User>.Success("User created", HttpStatusCode.Created , user);
    }
    public async Task<ApiResponse> UpdateAsync(Guid id, UserUpdateRequestDto request)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return ApiResponse.Fail("User not found", HttpStatusCode.NotFound);

        user = _mapper.Map<User>(request);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return ApiResponse.Fail(string.Join(", ", result.Errors.Select(e => e.Description)), HttpStatusCode.BadRequest);

        return ApiResponse.Success("User updated", HttpStatusCode.OK);
    }
    public async Task<ApiResponse> DeleteAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return ApiResponse.Fail("User not found", HttpStatusCode.NotFound);

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            return ApiResponse.Fail(string.Join(", ", result.Errors.Select(e => e.Description)), HttpStatusCode.BadRequest);

        return ApiResponse.Success("User deleted", HttpStatusCode.OK);
    }
}
