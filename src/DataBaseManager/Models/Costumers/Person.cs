using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Models.DataBaseModels.Costumers;

public class Person
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? Family { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? PhoneNumber { get; set; }

    public Guid RegionId { get; set; }
    public Region Region { get; set; } = default!;
}
