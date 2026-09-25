using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface ITargetingProfileRepository
{
    Task<TargetingProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TargetingProfile>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken);
    Task UpdateAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken);
    Task DeleteAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken);

    Task AddToAudienceSegmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAudienceSegmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToGeoRegionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromGeoRegionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToContentCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromContentCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDeviceCriteriaAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDeviceCriteriaAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
