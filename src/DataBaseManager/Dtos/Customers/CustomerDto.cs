using BineshSoloution.Attributes;
using BineshSoloution.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Dtos.Customers;

[DtoResourceType(typeof(Models.DataBaseModels.Costumers.Customer))]
public class CustomerDto
{
    [Required(ErrorMessage = "Customer ID is required.")]
    [Display(Name = "Customer")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Person information is required.")]
    [Display(Name = "Person")]
    public PersonDto Person { get; set; } = default!;

    [Display(Name = "Active")]
    public bool Active { get; set; }

    [Display(Name = "Customer Type")]
    public ICustumerType Type { get; set; }

    [Display(Name = "Description")]
    public string? Desc { get; set; }

    [Display(Name = "Payment Reliability")]
    public float PaymentReliability { get; set; }
}
