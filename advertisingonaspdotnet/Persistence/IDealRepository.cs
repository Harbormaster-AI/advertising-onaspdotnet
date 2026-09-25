using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IDealRepository
{
    Task<Deal?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Deal>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Deal deal, CancellationToken cancellationToken);
    Task UpdateAsync(Deal deal, CancellationToken cancellationToken);
    Task DeleteAsync(Deal deal, CancellationToken cancellationToken);

    Task AddToInventorySourcesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInventorySourcesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPlacementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPlacementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
