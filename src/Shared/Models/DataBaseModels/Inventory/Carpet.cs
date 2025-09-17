using Shared.Enum;
using Shared.Models.DataBaseModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DataBaseModels.Inventory;

public class Carpet : Product
{
    public Knot Knot { get; set; }  
    public int Density { get; set; }

}
