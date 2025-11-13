using BineshSoloution.Dtos.Account;
using BineshSoloution.Dtos.Inventory;
using BineshSoloution.Enum;

namespace BineshSoloution.Interfaces.Products;

public interface IProductService
{
    IQueryable<ProductDto> Get(); // used for querying (like your existing Get())
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken); // we have to test if the mapper can handle type casting propperly ... 
    Task<List<ProductDto>?> GetByInventoryCodeAsync(int inventoryCode, CancellationToken cancellationToken);
    Task<List<ProductDto>?> GetByCategoryAsync(ProductCategory category, CancellationToken cancellationToken);
    Task<List<ProductDto>?> GetByInventoryIdAndCategoryAsync(int inventoryId, ProductCategory category, CancellationToken cancellationToken);
    Task<ProductDto> CreateAsync(ProductDto dto, CancellationToken cancellationToken);

    Task<ProductDto> UpdateAsync(ProductDto dto, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

}