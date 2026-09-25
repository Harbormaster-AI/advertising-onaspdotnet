using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IPublisherRepository
{
    Task<Publisher?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Publisher>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Publisher publisher, CancellationToken cancellationToken);
    Task UpdateAsync(Publisher publisher, CancellationToken cancellationToken);
    Task DeleteAsync(Publisher publisher, CancellationToken cancellationToken);

    Task AddToInventorySourcesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInventorySourcesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDealsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDealsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCreativeApprovalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCreativeApprovalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInsertionOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInsertionOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRateCardsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRateCardsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
