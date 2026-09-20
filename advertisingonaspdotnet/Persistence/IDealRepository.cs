using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface IDealRepository
{
    Task<Deal?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Deal>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Deal deal, CancellationToken cancellationToken);
    Task UpdateAsync(Deal deal, CancellationToken cancellationToken);
    Task DeleteAsync(Deal deal, CancellationToken cancellationToken);
}
