
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class GeoRegionRepository : IGeoRegionRepository
{
    private readonly ApplicationDbContext _db;

    public GeoRegionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<GeoRegion?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.GeoRegions
            .Include(x => x.Parent)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<GeoRegion>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.GeoRegions
            .AsNoTracking()
            .Include(x => x.Parent)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(GeoRegion geoRegion, CancellationToken cancellationToken)
    {
        _db.GeoRegions.Add(geoRegion);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(GeoRegion geoRegion, CancellationToken cancellationToken)
    {
        _db.GeoRegions.Update(geoRegion);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(GeoRegion geoRegion, CancellationToken cancellationToken)
    {
        _db.GeoRegions.Remove(geoRegion);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToChildrenAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GeoRegions
            .Where(geoRegion =>
                request.ChildIds.Contains(geoRegion.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    geoRegion =>
                        EF.Property<Guid?>(
                            geoRegion,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromChildrenAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GeoRegions
            .Where(geoRegion =>
                request.ChildIds.Contains(geoRegion.Id) &&
                EF.Property<Guid?>(
                    geoRegion,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    geoRegion =>
                        EF.Property<Guid?>(
                            geoRegion,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
