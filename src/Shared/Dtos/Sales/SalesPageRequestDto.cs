using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Sales
{
    public class SalesPageRequestDto
    {
        public DateFilterDto DateFilter { get; set; } = default!;
        public CategoryFilterDto CategoryDto { get; set; } = default!;
        /*
         
         there are going to be some information take for the session , request datetime... and etc..
         
         */
    }

    public class CategoryFilterDto
    {
        public ProductCategory ProductCategory { get; set; }
    }

    public class DateFilterDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeFrameUnit TimeFrameUnit { get; set; }

    }
}
