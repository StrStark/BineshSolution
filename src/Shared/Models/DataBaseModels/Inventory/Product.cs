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
    public string? ProductCode { get; set; }
    public string? ProductDesc { get; set; }
    public string? ProductDesc2 { get; set; }
    public string? ProductDescBarcode { get; set; }
    public string? ProductDescLatin { get; set; }
    public bool ProductIsActive { get; set; }

    public Guid InventoryId{ get; set; }
    public Inventory Inventory { get; set; } = default!;
    public DateTime EnteryDate { get; set; } = default!;
    public DateTime ExitDate { get; set; } = default!;

    public float Cost { get; set; }
    public float SellingPrice { get; set; }
}

