using Shared.Enum;
using Shared.Models.DataBaseModels.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DataBaseModels.Sales;

public class Sales
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Product Product { get; set; } = default!;
    public DateTime Date { get; set; }
    public Invoice Invoice { get; set; } = default!;
    public float Incoming { get; set; }
    public float Outgoing { get; set; }
    public int RequestNumber { get; set; }
    public RequestState State { get; set; } = default!;
    public float DeliveredQuantity { get; set; }
    public Price Price { get; set; } = default!;
    public string? Region { get; set; }
}
