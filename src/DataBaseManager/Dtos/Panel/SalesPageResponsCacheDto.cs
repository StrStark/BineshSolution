using BineshSoloution.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Dtos.Panel;

public class SalesPageResponsCacheDto
{
    public SalesSummaryDto SalesSummary { get; set; } = default!;
    public SalesCardsDto SaleCardsDto { get; set; } = default!;
    public CategorizedSales CategorizedSales { get; set; } = default!;
    public RegionalSalesDto RegionalSales { get; set; } = default!;
}