using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface ICampaignRepository
{
    Task<Campaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Campaign>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Campaign campaign, CancellationToken cancellationToken);
    Task UpdateAsync(Campaign campaign, CancellationToken cancellationToken);
    Task DeleteAsync(Campaign campaign, CancellationToken cancellationToken);

    Task AddToLineItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLineItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToKpisAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromKpisAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTrackingPixelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTrackingPixelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAudiencesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAudiencesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
