using Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Sales;

[DtoResourceType(typeof(Models.DataBaseModels.Sales.Price))]
public class PriceDto
{
    [Required(ErrorMessage = "Price ID is required.")]
    [Display(Name = "Price")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Fee amount is required.")]
    [Display(Name = "Fee")]
    public long Fee { get; set; }

    [Display(Name = "Receipt")]
    public long Receipt { get; set; }

    [Display(Name = "Voucher")]
    public long Voucher { get; set; }
}
