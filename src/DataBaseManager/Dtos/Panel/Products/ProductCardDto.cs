namespace BineshSoloution.Dtos.Panel.Products;

public class ProductCardDto
{
    public Card TotalProdact { get; set; } = default!;
    public Card InventoryCirculationCount { get; set; } = default!;
    public Card InventoryCirculationDays { get; set; } = default!;  
}
