using DataBaseManager.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Dtos.Sales;

public class SalesRecordAddingRequestDto
{
    public Guid ProductId { get; set; }

    
    public InvoiceType InvoiceType { get; set; }
    public bool InvoiceRequest { get; set; }
    public bool InvoiceFlag { get; set; } 
    public int DocNumber { get; set; }
    public string? Counterparty { get; set; }

    
    public long PriceFee { get; set; }
    public long PriceReceipt { get; set; }
    public long PriceVoucher { get; set; }

    
    public RequestState State { get; set; }
    public DateTime Date { get; set; }
    public float Incoming { get; set; }
    public float Outgoing { get; set; }
    public int RequestNumber { get; set; }
    public float DeliveredQuantity { get; set; }
}
