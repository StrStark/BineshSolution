using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Dtos;

public class SignOutRequestDto
{
    [Required]
    public string RefreshToken { get; set; } = null!;
}
