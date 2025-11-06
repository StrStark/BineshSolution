using DataBaseManager.Enum;
using System.ComponentModel.DataAnnotations;

namespace DataBaseManager.Dtos.User;

public class UserUpdateRequestDto
{
    [StringLength(100)]
    public string? FullName { get; set; }

    [EmailAddress, StringLength(255)]
    public string? Email { get; set; }

    [Phone, StringLength(13)]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    public string? UserName { get; set; }

    public Gender? Gender { get; set; }

    [DataType(DataType.Date)]
    public DateTimeOffset? BirthDate { get; set; }

    [StringLength(255)]
    public string? ProfileImageName { get; set; }
}
