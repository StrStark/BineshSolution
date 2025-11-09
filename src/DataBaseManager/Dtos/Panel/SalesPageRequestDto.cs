using BineshSoloution.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Dtos.Panel
{
    public class SalesPageRequestDto
    {
        public DateFilterDto DateFilter { get; set; } = default!;
        public CategoryFilterDto CategoryDto { get; set; } = default!;
        public RequestProvience Provience { get; set; } = default!;
        /*
         
         there are going to be some information take for the session , request datetime... and etc..
         
         */
    }

    public class RequestProvience
    {
        public string? Provinece { get; set; } = default!; // later use enum for this
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
