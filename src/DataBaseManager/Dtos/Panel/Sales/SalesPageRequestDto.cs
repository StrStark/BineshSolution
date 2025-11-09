using BineshSoloution.Dtos.Filter;
using BineshSoloution.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Dtos.Panel.Sales;

public class SalesPageRequestDto
{
    public DateFilterDto DateFilter { get; set; } = default!;
    public CategoryFilterDto CategoryDto { get; set; } = default!;
    public RequestProvience Provience { get; set; } = default!;
}