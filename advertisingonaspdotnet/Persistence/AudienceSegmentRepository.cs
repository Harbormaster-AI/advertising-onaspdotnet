
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class AudienceSegmentRepository : IAudienceSegmentRepository
{
    private readonly ApplicationDbContext _db;

    public AudienceSegmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AudienceSegment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AudienceSegments
            .Include(x => x.Provider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AudienceSegment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AudienceSegments
            .AsNoTracking()
            .Include(x => x.Provider)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AudienceSegment audienceSegment, CancellationToken cancellationToken)
    {
        _db.AudienceSegments.Add(audienceSegment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AudienceSegment audienceSegment, CancellationToken cancellationToken)
    {
        _db.AudienceSegments.Update(audienceSegment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AudienceSegment audienceSegment, CancellationToken cancellationToken)
    {
        _db.AudienceSegments.Remove(audienceSegment);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCampaignsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Campaigns
            .Where(campaign =>
                request.ChildIds.Contains(campaign.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    campaign =>
                        EF.Property<Guid?>(
                            campaign,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCampaignsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Campaigns
            .Where(campaign =>
                request.ChildIds.Contains(campaign.Id) &&
                EF.Property<Guid?>(
                    campaign,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    campaign =>
                        EF.Property<Guid?>(
                            campaign,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
