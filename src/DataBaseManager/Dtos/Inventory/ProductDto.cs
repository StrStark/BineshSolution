using BineshSoloution.Attributes;
using BineshSoloution.Models.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Dtos.Inventory;

[DtoResourceType(typeof(Product))]
public class ProductDto
{
    [Required(ErrorMessage = "Product ID is required.")]
    [Display(Name = "Product")]
    public Guid Id { get; set; }

    [Display(Name = "Manufacturer")]
    public string? Manufacturer { get; set; }

    [Display(Name = "Product Code")]
    public string? ProductCode { get; set; }

    [Display(Name = "Product Description")]
    public string? ProductDesc { get; set; }

    [Display(Name = "Additional Description")]
    public string? ProductDesc2 { get; set; }

    [Display(Name = "Barcode Description")]
    public string? ProductDescBarcode { get; set; }

    [Display(Name = "Latin Description")]
    public string? ProductDescLatin { get; set; }

    [Display(Name = "Active Status")]
    public bool ProductIsActive { get; set; }

    [Required(ErrorMessage = "Inventory information is required.")]
    [Display(Name = "Inventory")]
    public Guid InventoryId { get; set; } = default!;

    [Required(ErrorMessage = "Entry date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
    [Display(Name = "Entry Date")]
    public DateTime EnteryDate { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
    [Display(Name = "Exit Date")]
    public DateTime ExitDate { get; set; }

    [Required(ErrorMessage = "Cost is required.")]
    [Display(Name = "Cost")]
    public float Cost { get; set; }

    [Required(ErrorMessage = "Selling price is required.")]
    [Display(Name = "Selling Price")]
    public float SellingPrice { get; set; }
}