namespace BineshSoloution.Dtos.Panel.Products;

public class ProductsDetails
{
    public int TotalBuy { get; set; } = default!;
    public int TotalSell { get; set; } = default!;
    public int TotalInventoryy { get; set; } = default!;
    public List<ProductRecords> Records { get; set; } = default!;
}