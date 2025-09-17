using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DataBaseModels.Sales;

public class Invoice
{
    public InvoiceType Type{ get; set; }
    public bool Request { get; set; }
    public bool invoice { get; set; }
    public int DocNumber { get; set; }
    public string? Counterparty { get; set; }


}
