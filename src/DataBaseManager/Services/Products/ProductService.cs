using AutoMapper;
using BineshSoloution.DbContexts;
using BineshSoloution.Dtos.Account;
using BineshSoloution.Dtos.Inventory;
using BineshSoloution.Enum;
using BineshSoloution.Exceptions;
using BineshSoloution.Interfaces.Products;
using BineshSoloution.Services.Account;
using Microsoft.EntityFrameworkCore;

namespace BineshSoloution.Services.Products;

public partial class ProductService : IProductService
{

    [AutoInject] private readonly ApplicationDbContext _appDbContext = default!;
    [AutoInject] private readonly IMapper _mapper = default!;
    [AutoInject] private readonly ILogger<ProductService> _logger = default!;
    public IQueryable<ProductDto> Get()
    {
        return _appDbContext.Products.Select(s => _mapper.Map<ProductDto>(s));
    }
    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Products.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        return entity is null ? null : _mapper.Map<ProductDto>(entity);
    }
    public async Task<ProductDto> CreateAsync(ProductDto dto, CancellationToken cancellationToken)
    {
        dto.Id = Guid.NewGuid();
        var entity = _mapper.Map<Models.Inventory.Product>(dto);

        await _appDbContext.Products.AddAsync(entity, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(entity);
    }
    public async Task<ProductDto> UpdateAsync(ProductDto dto, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Products.FirstOrDefaultAsync(s => s.Id == dto.Id, cancellationToken)
                    ?? throw new ResourceNotFoundException($"Product record with id {dto.Id} not found.");

        _mapper.Map(dto, entity);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(entity);
    }
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var affectedRows = await _appDbContext.Products
             .Where(s => s.Id == id)
             .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows < 1)
            throw new ResourceNotFoundException($"Product record with id {id} not found.");
    }

    public async Task<ProductDto?> GetByInventoryCodeAsync(int inventoryCode, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Products.Include(p => p.Inventory).Where(p => p.Inventory.Code == inventoryCode).ToListAsync(); ;

        return _mapper.Map<ProductDto?>(entity);

    }

    public Task<ProductDto?> GetByCategoryAsync(ProductCategory category, CancellationToken cancellationToken)
    {
        var entity =  _appDbContext.Products.Where(p => p.GetType().ToString() == category.ToString()).ToListAsync(cancellationToken); // might not work

        return _mapper.Map<Task<ProductDto?>>(entity);

    }

    public async Task<ProductDto?> GetByInventoryIdAndCategoryAsync(int inventoryCode, ProductCategory category, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Products.Include(p => p.Inventory).Where(p => p.Inventory.Code == inventoryCode && p.GetType().ToString()== category.ToString()).ToListAsync(); 
    
        return _mapper.Map<ProductDto?>(entity);
    }
}

