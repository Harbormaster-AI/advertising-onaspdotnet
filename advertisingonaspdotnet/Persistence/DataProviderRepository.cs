
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class DataProviderRepository : IDataProviderRepository
{
    private readonly ApplicationDbContext _db;

    public DataProviderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DataProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DataProviders
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DataProvider>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DataProviders
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DataProvider dataProvider, CancellationToken cancellationToken)
    {
        _db.DataProviders.Add(dataProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DataProvider dataProvider, CancellationToken cancellationToken)
    {
        _db.DataProviders.Update(dataProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DataProvider dataProvider, CancellationToken cancellationToken)
    {
        _db.DataProviders.Remove(dataProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAudienceSegmentsAsync(
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

    public async Task RemoveFromAudienceSegmentsAsync(
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

}
