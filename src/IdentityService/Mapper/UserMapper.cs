using IdentityService.Models;
using Shared.Dtos.User;
using Shared.Enum;
using Twilio.Types;

namespace IdentityService.Mapper;

public static class UserMapper
{
    public static User ToEntity(UserCreateRequestDto dto)
    {
        return new User
        {
            UserName = dto.UserName,
            PhoneNumber = dto.PhoneNumber,
            LockoutEnabled = true
        };
    }
    public static User ToEntity(UserUpdateRequestDto dto , User User)
    {
        User.UserName = dto.UserName;
        User.PhoneNumber = dto.PhoneNumber;
        User.FullName = dto.FullName;
        User.Gender = dto.Gender;
        User.ProfileImageName = dto.ProfileImageName;
        User.BirthDate = dto.BirthDate;
        User.Email = dto.Email;
        return User;
    }
}
