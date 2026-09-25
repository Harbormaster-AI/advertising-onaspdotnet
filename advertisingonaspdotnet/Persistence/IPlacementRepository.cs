using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IPlacementRepository
{
    Task<Placement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Placement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Placement placement, CancellationToken cancellationToken);
    Task UpdateAsync(Placement placement, CancellationToken cancellationToken);
    Task DeleteAsync(Placement placement, CancellationToken cancellationToken);


}
