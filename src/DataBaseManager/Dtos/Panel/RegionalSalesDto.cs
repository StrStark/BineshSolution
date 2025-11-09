namespace BineshSoloution.Dtos.Panel;

public class RegionalSalesDto
{
    public List<SaleOverRegionDto> SaleOverRegion { get; set; } = default!;
    public Int64 TotalSale { get; set; }
    public float GrowthrRate { get; set; } // a number between 0 and 100
}