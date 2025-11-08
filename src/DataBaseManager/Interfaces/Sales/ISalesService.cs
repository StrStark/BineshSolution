using BineshSoloution.Dtos.Sales;

namespace BineshSoloution.Interfaces.Sales;

public interface ISalesService
{
    IQueryable<SalesDto> Get(); // used for querying (like your existing Get())

    Task<SalesDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<SalesDto?>> GetByDateDiffrenceAsync(DateTime Start, DateTime End, CancellationToken cancellationToken);

    Task<SalesDto> CreateAsync(SalesDto dto, CancellationToken cancellationToken);

    Task<SalesDto> UpdateAsync(SalesDto dto, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
