using Shared.Attributes;
using Shared.Dtos.Inventory;
using Shared.Models.DataBaseModels.Inventory;
using Shared.Models.DataBaseModels.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Sales;

[DtoResourceType(typeof(Models.DataBaseModels.Sales.Sales))]
public class SalesDto
{
    [Required(ErrorMessage = "Sale ID is required.")]
    [Display(Name = "Sale")]

    public Guid Id { get; set; }
    [Display(Name = "Product")]
    public Guid? ProductId { get; set; }

    [Display(Name = "Carpet? Details")]
    public CarpetDto? Carpet { get; set; }

    [Display(Name = "Rug? Details")]
    public RugDto? Rug { get; set; }

    [Display(Name = "Raw? Material Details")]
    public RawMaterialDto? RawMaterial { get; set; }

    [Required(ErrorMessage = "Invoice information is required.")]
    [Display(Name = "Invoice")]
    public InvoiceDto Invoice { get; set; } = default!;

    [Required(ErrorMessage = "Price information is required.")]
    [Display(Name = "Price")]
    public PriceDto Price { get; set; } = default!;

    [Required(ErrorMessage = "Date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
    [Display(Name = "Date")]
    public DateTime Date { get; set; }

    [Range(0, float.MaxValue, ErrorMessage = "Delivered quantity must be zero or greater.")]
    [Display(Name = "Delivered Quantity")]
    public float DeliveredQuantity { get; set; }
}