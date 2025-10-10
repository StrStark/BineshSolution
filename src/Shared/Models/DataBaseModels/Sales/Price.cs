using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DataBaseModels.Sales;

public class Price
{
    public Guid Id { get; set; }

    public Int64 Fee { get; set; }
    public Int64 Receipt { get; set; }
    public Int64 Voucher { get; set; }
}
