using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DataBaseModels.Account.DocType;

public class Cashpayment : Doc
{
    public string? DebitCode { get; set; }
    public string? CreditCode { get; set; }

}
