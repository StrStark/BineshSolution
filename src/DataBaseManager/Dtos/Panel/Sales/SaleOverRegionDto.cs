namespace BineshSoloution.Dtos.Panel.Sales;

public class SaleOverRegionDto
{
    public string? City { get; set; }
    public Int64 SalesPrice { get; set; }
    public float GrowthrRate { get; set; } // a number between 0 and 100
}
