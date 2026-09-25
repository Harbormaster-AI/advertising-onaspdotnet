
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class TrackingPixelRepository : ITrackingPixelRepository
{
    private readonly ApplicationDbContext _db;

    public TrackingPixelRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TrackingPixel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TrackingPixels
            .Include(x => x.Campaign)
            .Include(x => x.Advertiser)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TrackingPixel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TrackingPixels
            .AsNoTracking()
            .Include(x => x.Campaign)
            .Include(x => x.Advertiser)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TrackingPixel trackingPixel, CancellationToken cancellationToken)
    {
        _db.TrackingPixels.Add(trackingPixel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TrackingPixel trackingPixel, CancellationToken cancellationToken)
    {
        _db.TrackingPixels.Update(trackingPixel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TrackingPixel trackingPixel, CancellationToken cancellationToken)
    {
        _db.TrackingPixels.Remove(trackingPixel);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToConversionEventsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ConversionEvents
            .Where(conversionEvent =>
                request.ChildIds.Contains(conversionEvent.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    conversionEvent =>
                        EF.Property<Guid?>(
                            conversionEvent,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromConversionEventsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ConversionEvents
            .Where(conversionEvent =>
                request.ChildIds.Contains(conversionEvent.Id) &&
                EF.Property<Guid?>(
                    conversionEvent,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    conversionEvent =>
                        EF.Property<Guid?>(
                            conversionEvent,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
