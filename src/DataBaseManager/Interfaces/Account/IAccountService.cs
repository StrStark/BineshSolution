using BineshSoloution.Dtos.Account;
using BineshSoloution.Dtos.Sales;

namespace BineshSoloution.Interfaces.Account;

public interface IAccountService
{
    IQueryable<AccountDto> Get(); // used for querying (like your existing Get())

    Task<AccountDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<AccountDto?>> GetByDateDiffrenceAsync(DateTime Start, DateTime End, CancellationToken cancellationToken);

    Task<AccountDto> CreateAsync(AccountDto dto, CancellationToken cancellationToken);

    Task<AccountDto> UpdateAsync(AccountDto dto, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
