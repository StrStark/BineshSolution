using Microsoft.AspNetCore.Identity;
using DataBaseManager.Enum;
using System.Reflection;

namespace DataBaseManager.Models.AuthModels;

public class User : IdentityUser<Guid>
{
    [PersonalData]
    public override Guid Id { get; set; } = default!;
    [PersonalData]
    public string? FullName { get; set; }

    public string? DisplayName => FullName ?? Email ?? PhoneNumber ?? UserName;

    [PersonalData]
    public Gender? Gender { get; set; }

    [PersonalData]
    public DateTimeOffset? BirthDate { get; set; }

    [PersonalData]
    public string? ProfileImageName { get; set; }

    [PersonalData]
    public List<UserSession>? Sessions { get; set; } = new List<UserSession>();
    public DateTimeOffset? EmailTokenRequestedOn { get; set; }

    public DateTimeOffset? PhoneNumberTokenRequestedOn { get; set; }

}
