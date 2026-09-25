using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IAgencyRepository
{
    Task<Agency?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Agency>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Agency agency, CancellationToken cancellationToken);
    Task UpdateAsync(Agency agency, CancellationToken cancellationToken);
    Task DeleteAsync(Agency agency, CancellationToken cancellationToken);

    Task AddToAdvertisersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAdvertisersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTeamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTeamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToUsersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromUsersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInsertionOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInsertionOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
