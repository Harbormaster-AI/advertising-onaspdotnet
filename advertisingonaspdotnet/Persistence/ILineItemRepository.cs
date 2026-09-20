using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface ILineItemRepository
{
    Task<LineItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LineItem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(LineItem lineItem, CancellationToken cancellationToken);
    Task UpdateAsync(LineItem lineItem, CancellationToken cancellationToken);
    Task DeleteAsync(LineItem lineItem, CancellationToken cancellationToken);
}
