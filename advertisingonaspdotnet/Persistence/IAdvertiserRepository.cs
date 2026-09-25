using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IAdvertiserRepository
{
    Task<Advertiser?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Advertiser>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Advertiser advertiser, CancellationToken cancellationToken);
    Task UpdateAsync(Advertiser advertiser, CancellationToken cancellationToken);
    Task DeleteAsync(Advertiser advertiser, CancellationToken cancellationToken);

    Task AddToAdAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAdAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToBillingProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBillingProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCampaignsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCampaignsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTrackingPixelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTrackingPixelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
