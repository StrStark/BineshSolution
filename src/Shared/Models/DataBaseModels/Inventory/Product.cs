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
    public Guid Id { get; set; }
    public string? Manufacturer { get; set; }
    public string? InventoryCode { get; set; }
    public string? InventoryDesc { get; set; }
    public string? InventoryDesc2 { get; set; }
    public string? InventoryDescBarcode { get; set; }
    public string? InventoryDescLatin { get; set; }
    public bool InventoryIsActive { get; set; }
}

