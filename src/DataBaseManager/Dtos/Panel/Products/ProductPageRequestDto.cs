using BineshSoloution.Dtos.Filter;

namespace BineshSoloution.Dtos.Panel.Products;

public class ProductPageRequestDto
{
    public CategoryFilterDto CategoryDto { get; set; } = default!;
    public InventoryFilterDto InventoryDto { get; set; } = default!;
}

