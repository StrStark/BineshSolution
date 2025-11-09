using BineshSoloution.Enum;

namespace BineshSoloution.Dtos.Panel.Products;

public class ProductItem
{
    public string ProductName { get; set; } = default!;
    public string ManufacturingCode { get; set; } = default!;
    public ProductCategory Category { get; set; } = default!;
    public Int64 PriceUnit { get; set; } = default!;
    public Int64 TotalSale { get; set; } = default!;
    public ProductsDetails Details { get; set; } = default!;
}