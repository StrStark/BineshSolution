using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Models.DataBaseModels.Inventory;

public class Receipt
{
    public Guid Id { get; set; }

    public string? ProductCode { get; set; }
    public int Number { get; set; }
    public DateTime Date { get; set; }
    public string? Descryption { get; set; }
    public string? ReceiptType { get; set; } // change to enum
    public string? Reciver { get; set; }
    public string? Sender { get; set; }
    public string? Status { get; set; } // change to enum
    public Int64 Price { get; set; }
    public string? CreatedBy { get; set; }
}
