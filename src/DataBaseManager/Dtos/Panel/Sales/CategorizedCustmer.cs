using BineshSoloution.Enum;

namespace BineshSoloution.Dtos.Panel.Sales;

public class CategorizedCustmer
{
    public ICustumerType Type { get; set; }
    public int Count { get; set; }
    public DateTime OnDate { get; set; } // could be monthly , dail , or yearly based on the filter!

}