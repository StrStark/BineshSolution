using DataBaseManager.Enum;
using DataBaseManager.Models.DataBaseModels.Costumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Models.DataBaseModels.Sales;

public class Invoice
{
    public Guid Id { get; set; }

    public InvoiceType Type{ get; set; }
    public bool Request { get; set; }
    public bool invoice { get; set; }
    public int DocNumber { get; set; }

    public Guid CounterpartyId { get; set; }
    public Customer Counterparty { get; set; } = default!;
}
