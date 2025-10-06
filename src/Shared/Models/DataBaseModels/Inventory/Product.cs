using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models.DataBaseModels.Sales;

namespace Shared.Models.DataBaseModels.Inventory;
public class Product
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Manufacturer { get; set; }
    public string? InventoryCode { get; set; }
    public string? InventoryDesc { get; set; }
    public string? InventoryDesc2 { get; set; }
    public string? InventoryDescBarcode { get; set; }
    public string? InventoryDescLatin { get; set; }
    public bool InventoryIsActive { get; set; }
    public ICollection<Sales.Sales> Sales { get; set; } = new List<Sales.Sales>();
}

