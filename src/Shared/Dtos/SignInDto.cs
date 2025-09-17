using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos;

public class SignInDto
{
    public virtual string? PhoneNumber { get; set; }
    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(PhoneNumber))
            yield return new ValidationResult(errorMessage: "the Phone Number is not valid", [nameof(PhoneNumber)]);
    }
}
