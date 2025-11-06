using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Dtos;

public class SignUpRequestDto 
{
    [Phone, StringLength(13)]
    public string? PhoneNumber { get; set; } = default!;
}
