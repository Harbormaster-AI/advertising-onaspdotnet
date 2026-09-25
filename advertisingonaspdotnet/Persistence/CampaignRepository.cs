
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class CampaignRepository : ICampaignRepository
{
    private readonly ApplicationDbContext _db;

    public CampaignRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Campaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Campaigns
            .Include(x => x.AdAccount)
            .Include(x => x.InsertionOrder)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Campaign>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Campaigns
            .AsNoTracking()
            .Include(x => x.AdAccount)
            .Include(x => x.InsertionOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        _db.Campaigns.Add(campaign);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        _db.Campaigns.Update(campaign);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        _db.Campaigns.Remove(campaign);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToLineItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LineItems
            .Where(lineItem =>
                request.ChildIds.Contains(lineItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    lineItem =>
                        EF.Property<Guid?>(
                            lineItem,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLineItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LineItems
            .Where(lineItem =>
                request.ChildIds.Contains(lineItem.Id) &&
                EF.Property<Guid?>(
                    lineItem,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    lineItem =>
                        EF.Property<Guid?>(
                            lineItem,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToKpisAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.KPIs
            .Where(kPI =>
                request.ChildIds.Contains(kPI.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    kPI =>
                        EF.Property<Guid?>(
                            kPI,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromKpisAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.KPIs
            .Where(kPI =>
                request.ChildIds.Contains(kPI.Id) &&
                EF.Property<Guid?>(
                    kPI,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    kPI =>
                        EF.Property<Guid?>(
                            kPI,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToTrackingPixelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TrackingPixels
            .Where(trackingPixel =>
                request.ChildIds.Contains(trackingPixel.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    trackingPixel =>
                        EF.Property<Guid?>(
                            trackingPixel,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTrackingPixelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TrackingPixels
            .Where(trackingPixel =>
                request.ChildIds.Contains(trackingPixel.Id) &&
                EF.Property<Guid?>(
                    trackingPixel,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    trackingPixel =>
                        EF.Property<Guid?>(
                            trackingPixel,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToAudiencesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AudienceSegments
            .Where(audienceSegment =>
                request.ChildIds.Contains(audienceSegment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    audienceSegment =>
                        EF.Property<Guid?>(
                            audienceSegment,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAudiencesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AudienceSegments
            .Where(audienceSegment =>
                request.ChildIds.Contains(audienceSegment.Id) &&
                EF.Property<Guid?>(
                    audienceSegment,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    audienceSegment =>
                        EF.Property<Guid?>(
                            audienceSegment,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToReportsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Reports
            .Where(report =>
                request.ChildIds.Contains(report.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    report =>
                        EF.Property<Guid?>(
                            report,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromReportsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Reports
            .Where(report =>
                request.ChildIds.Contains(report.Id) &&
                EF.Property<Guid?>(
                    report,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    report =>
                        EF.Property<Guid?>(
                            report,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
