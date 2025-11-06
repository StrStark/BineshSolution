using DataBaseManager.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Microsoft.AspNetCore.Identity;
public static class UserManagerExtensions
{
    public static async Task<User?> FindUserAsync(this UserManager<User> userManager, SignInDto identity)
    {
        User? user = default;

        var phoneNumber = identity.PhoneNumber;

        if (phoneNumber is null)
            throw new InvalidOperationException();

        if (string.IsNullOrEmpty(phoneNumber) is false)
        {
            user = await userManager.FindByNameAsync(phoneNumber!);
        }

        return user;
    }

    public static Task<User?> FindByPhoneNumber(this UserManager<User> userManager, string phoneNumber)
    {
        return userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
    }
}
