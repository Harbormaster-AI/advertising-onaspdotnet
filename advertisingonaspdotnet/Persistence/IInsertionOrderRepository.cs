using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IInsertionOrderRepository
{
    Task<InsertionOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsertionOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InsertionOrder insertionOrder, CancellationToken cancellationToken);
    Task UpdateAsync(InsertionOrder insertionOrder, CancellationToken cancellationToken);
    Task DeleteAsync(InsertionOrder insertionOrder, CancellationToken cancellationToken);

    Task AddToCampaignsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCampaignsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
