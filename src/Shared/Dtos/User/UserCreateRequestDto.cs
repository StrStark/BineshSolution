using Shared.Enum;
using System.ComponentModel.DataAnnotations;


namespace Shared.Dtos.User;

public class UserCreateRequestDto
{

    [Phone, StringLength(13)]
    public string? PhoneNumber { get; set; }

    [Required, StringLength(100)]
    public string? UserName { get; set; }

}
