﻿using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Sales
{
    public class SalesPageResponsDto
    {
        public FinancialSummaryDto SalesSummary { get; set; } = default!;
        public CategorizedSales CategorizedSales { get; set; } = default!;
        public SalesTarget SalesTarget { get; set; } = default!;
        public RegionalSalesDto RegionalSales { get; set; } = default!;

    }
    public class SalesTarget
    {
    }

    public class RegionalSalesDto
    {
        public List<SaleOverRegionDto> SaleOverRegion { get; set; } = default!;
    }

    public class SaleOverRegionDto
    {
        public string? Provience { get; set; } // we should use enums....
        public string? City { get; set; }
        public Int64 SalesPrice { get; set; }
        public int GrowthrRate { get; set; } // a number between 0 and 100
    }

    public class CategorizedSales
    {
        public List<CustomerCategorizedSalesDto> Sales { get; set; } = default!;
     }
    public class CustomerCategorizedSalesDto
    {
        public List<CategorizedCustmer> CategorizedSales { get; set; } = default!;
        public DateTime OnDate { get; set; } // could be monthly , dail , or yearly based on the filter!
    }
    public class CategorizedCustmer
    {
        public ICustumerType Type { get; set; }
        public int Count { get; set; }
    }

    public class FinancialSummaryDto
    {
        public List<SoldItem> SoldItems { get; set; } = default!;
        public List<ReturnItem> ReturnItems { get; set; } = default!;

        public int Count => SoldItems.Count + ReturnItems.Count;
        public Int64 TotalSales => SoldItems.Sum(item => item.Value) - ReturnItems.Sum(item => item.Value);
        public Int64 ReturnTotal => ReturnItems.Sum(item => item.Value);
        public Int64 OffSales { get; set; }
        public Int64 NewModelsSales { get; set; }
    }

    public class SoldItem
    {
        public string? Type { get; set; } // we will use enums after...
        public Int64 Value { get; set; }
    }
    public class ReturnItem
    {
        public string? Type { get; set; } // we will use enums after...
        public Int64 Value { get; set; }

    }
}

