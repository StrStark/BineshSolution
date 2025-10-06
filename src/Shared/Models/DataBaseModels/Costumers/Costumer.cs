using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Enum;

namespace Shared.Models.DataBaseModels.Costumers;

public class Costumer
{
    public Person Person { get; set; } = default!;
    public bool Active { get; set; }
    public ICostumerType Type{ get; set; }
    public string? Desc { get; set; }

}
