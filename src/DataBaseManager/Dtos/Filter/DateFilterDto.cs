using BineshSoloution.Enum;

namespace BineshSoloution.Dtos.Filter;

public class DateFilterDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeFrameUnit TimeFrameUnit { get; set; }

}
