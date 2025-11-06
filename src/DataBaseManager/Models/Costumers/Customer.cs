using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseManager.Enum;

namespace DataBaseManager.Models.DataBaseModels.Costumers;

public class Customer
{
    public Guid Id { get; set; }

    public Guid PersonId { get; set; }
    public Person Person { get; set; } = default!;
   
    public bool Active { get; set; }
    public ICustumerType Type{ get; set; }
    public string? Desc { get; set; }
    public float PaymentReliability { get; set; }
}
