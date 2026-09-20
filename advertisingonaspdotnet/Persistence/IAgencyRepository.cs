using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface IAgencyRepository
{
    Task<Agency?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Agency>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Agency agency, CancellationToken cancellationToken);
    Task UpdateAsync(Agency agency, CancellationToken cancellationToken);
    Task DeleteAsync(Agency agency, CancellationToken cancellationToken);
}
