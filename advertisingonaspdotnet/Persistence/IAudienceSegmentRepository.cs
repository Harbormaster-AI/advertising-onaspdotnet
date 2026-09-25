using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IAudienceSegmentRepository
{
    Task<AudienceSegment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AudienceSegment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AudienceSegment audienceSegment, CancellationToken cancellationToken);
    Task UpdateAsync(AudienceSegment audienceSegment, CancellationToken cancellationToken);
    Task DeleteAsync(AudienceSegment audienceSegment, CancellationToken cancellationToken);

    Task AddToCampaignsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCampaignsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
