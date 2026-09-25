using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IInventorySourceRepository
{
    Task<InventorySource?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InventorySource>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InventorySource inventorySource, CancellationToken cancellationToken);
    Task UpdateAsync(InventorySource inventorySource, CancellationToken cancellationToken);
    Task DeleteAsync(InventorySource inventorySource, CancellationToken cancellationToken);

    Task AddToAdSlotsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAdSlotsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDealsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDealsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
