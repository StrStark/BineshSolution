namespace BineshSoloution.Dtos.Panel.Sales;

public class SalesSummaryDto
{
    public List<SoldItem> SoldItems { get; set; } = default!;
    public List<ReturnItem> ReturnItems { get; set; } = default!;

    public int Count { get; set; }
    public Int64 Sum { get; set; }

}