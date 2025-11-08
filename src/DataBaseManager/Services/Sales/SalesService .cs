using AutoMapper;
using BineshSoloution.DbContexts;
using BineshSoloution.Dtos.Inventory;
using BineshSoloution.Dtos.Sales;
using BineshSoloution.Exceptions;
using BineshSoloution.Interfaces.Sales;
using BineshSoloution.Models.DataBaseModels.Sales;
using BineshSoloution.Models.Sales;
using Microsoft.EntityFrameworkCore;
using System;

namespace BineshSoloution.Services.Sales;

public partial class SalesService : ISalesService
{
    [AutoInject] private readonly ApplicationDbContext _appDbContext = default!;
    [AutoInject] private readonly IMapper _mapper = default!;
    [AutoInject] private readonly ILogger<SalesService> _logger = default!;

    public IQueryable<SalesDto> Get()
    {
        return _appDbContext.Sales.Select(s => _mapper.Map<SalesDto>(s));
    }
    public async Task<SalesDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Sales.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        return entity is null ? null : _mapper.Map<SalesDto>(entity);
    }
    public async Task<SalesDto> CreateAsync(SalesDto dto, CancellationToken cancellationToken)
    {
        var productTypeCount = new ProductDto[] { dto.Carpet!, dto.Rug!, dto.RawMaterial! }
            .Count(x => x is not null);

        if (!(dto.ProductId == Guid.Empty || dto.ProductId == null) && productTypeCount > 0)
            throw new InvalidOperationException("Cannot specify both ProductId and a product detail (Carpet/Rug/RawMaterial).");

        if ((dto.ProductId == Guid.Empty || dto.ProductId == null) && productTypeCount != 1)
            throw new InvalidOperationException("When ProductId is not provided, exactly one product detail (Carpet, Rug, or RawMaterial) must be present.");

        dto.Id = Guid.NewGuid();
        var entity = _mapper.Map <Models.Sales.Sales>(dto);

        await _appDbContext.Sales.AddAsync(entity, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SalesDto>(entity);
    }
    public async Task<SalesDto> UpdateAsync(SalesDto dto, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Sales.FirstOrDefaultAsync(s => s.Id == dto.Id, cancellationToken)
            ?? throw new ResourceNotFoundException($"Sale record with id {dto.Id} not found.");

        _mapper.Map(dto, entity);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SalesDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var affectedRows = await _appDbContext.Sales
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows < 1)
            throw new ResourceNotFoundException($"Sale record with id {id} not found.");
    }

    public async Task<List<SalesDto?>> GetByDateDiffrenceAsync(DateTime Start, DateTime End, CancellationToken cancellationToken)
    {
        var entities = await _appDbContext.Sales.Where(i => i.Date >= Start && i.Date <= End).Include(p => p.Price).Include(i => i.Invoice).Include(p => p.Product).ToListAsync(cancellationToken);
        
        return _mapper.Map<List<SalesDto?>>(entities);

    }
}
