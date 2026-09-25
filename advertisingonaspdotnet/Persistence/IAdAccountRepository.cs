using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IAdAccountRepository
{
    Task<AdAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AdAccount>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AdAccount adAccount, CancellationToken cancellationToken);
    Task UpdateAsync(AdAccount adAccount, CancellationToken cancellationToken);
    Task DeleteAsync(AdAccount adAccount, CancellationToken cancellationToken);

    Task AddToUsersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromUsersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCampaignsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCampaignsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPerformanceMetricsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPerformanceMetricsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
