using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface ILineItemRepository
{
    Task<LineItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LineItem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(LineItem lineItem, CancellationToken cancellationToken);
    Task UpdateAsync(LineItem lineItem, CancellationToken cancellationToken);
    Task DeleteAsync(LineItem lineItem, CancellationToken cancellationToken);

    Task AddToPlacementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPlacementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCreativesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCreativesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPerformanceMetricsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPerformanceMetricsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
