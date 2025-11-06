using DataBaseManager.Enum;
using DataBaseManager.Models.DataBaseModels.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Models.DataBaseModels.Sales;

public class Sales
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
   
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = default!;
    
    public Guid PriceId { get; set; }
    public Price Price { get; set; } = default!;

    public RequestState State { get; set; } = default!;
    public DateTime Date { get; set; }
    public float Incoming { get; set; }
    public float Outgoing { get; set; }
    public int RequestNumber { get; set; }
    public float DeliveredQuantity { get; set; }
}
