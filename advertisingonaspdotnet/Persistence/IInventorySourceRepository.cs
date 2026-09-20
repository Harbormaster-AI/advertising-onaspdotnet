using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface IInventorySourceRepository
{
    Task<InventorySource?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InventorySource>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InventorySource inventorySource, CancellationToken cancellationToken);
    Task UpdateAsync(InventorySource inventorySource, CancellationToken cancellationToken);
    Task DeleteAsync(InventorySource inventorySource, CancellationToken cancellationToken);
}
