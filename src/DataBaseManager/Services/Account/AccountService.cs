using AutoMapper;
using BineshSoloution.DbContexts;
using BineshSoloution.Dtos.Account;
using BineshSoloution.Dtos.Inventory;
using BineshSoloution.Dtos.Sales;
using BineshSoloution.Exceptions;
using BineshSoloution.Interfaces.Account;
using BineshSoloution.Services.Sales;
using Microsoft.EntityFrameworkCore;

namespace BineshSoloution.Services.Account;

public partial class AccountService : IAccountService
{
    [AutoInject] private readonly ApplicationDbContext _appDbContext = default!;
    [AutoInject] private readonly IMapper _mapper = default!;
    [AutoInject] private readonly ILogger<AccountService> _logger = default!;

    public IQueryable<AccountDto> Get()
    {
        return _appDbContext.Accounts.Select(s => _mapper.Map<AccountDto>(s));
    }
    public async Task<AccountDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Accounts.FirstOrDefaultAsync(t => t.ID == id, cancellationToken);

        return entity is null ? null : _mapper.Map<AccountDto>(entity);
    }
    public async Task<AccountDto> CreateAsync(AccountDto dto, CancellationToken cancellationToken)
    {
        dto.Id = Guid.NewGuid();
        var entity = _mapper.Map<Models.Sales.Sales>(dto);

        await _appDbContext.Sales.AddAsync(entity, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AccountDto>(entity);
    }
    public async Task<AccountDto> UpdateAsync(AccountDto dto, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Accounts.FirstOrDefaultAsync(s => s.ID == dto.Id, cancellationToken)
            ?? throw new ResourceNotFoundException($"Account record with id {dto.Id} not found.");

        _mapper.Map(dto, entity);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AccountDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var affectedRows = await _appDbContext.Sales
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows < 1)
            throw new ResourceNotFoundException($"Sale record with id {id} not found.");
    }

    public async Task<List<AccountDto?>> GetByDateDiffrenceAsync(DateTime Start, DateTime End, CancellationToken cancellationToken)
    {
        var entities = await _appDbContext.Accounts.Where(i => i.Date >= Start && i.Date <= End).Include(p => p.SubAccounts).ToListAsync(cancellationToken);

        return _mapper.Map<List<AccountDto?>>(entities);

    }

    public async Task<AccountDto?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Accounts.FirstOrDefaultAsync(A => A.Name == name, cancellationToken);

        return _mapper?.Map<AccountDto?>(entity);
    }
}
