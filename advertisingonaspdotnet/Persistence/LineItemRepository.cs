
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class LineItemRepository : ILineItemRepository
{
    private readonly ApplicationDbContext _db;

    public LineItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<LineItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.LineItems
            .Include(x => x.Campaign)
            .Include(x => x.TargetingProfile)
            .Include(x => x.Deal)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LineItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.LineItems
            .AsNoTracking()
            .Include(x => x.Campaign)
            .Include(x => x.TargetingProfile)
            .Include(x => x.Deal)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LineItem lineItem, CancellationToken cancellationToken)
    {
        _db.LineItems.Add(lineItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LineItem lineItem, CancellationToken cancellationToken)
    {
        _db.LineItems.Update(lineItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LineItem lineItem, CancellationToken cancellationToken)
    {
        _db.LineItems.Remove(lineItem);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPlacementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Placements
            .Where(placement =>
                request.ChildIds.Contains(placement.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    placement =>
                        EF.Property<Guid?>(
                            placement,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPlacementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Placements
            .Where(placement =>
                request.ChildIds.Contains(placement.Id) &&
                EF.Property<Guid?>(
                    placement,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    placement =>
                        EF.Property<Guid?>(
                            placement,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToCreativesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CreativeAssets
            .Where(creativeAsset =>
                request.ChildIds.Contains(creativeAsset.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    creativeAsset =>
                        EF.Property<Guid?>(
                            creativeAsset,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCreativesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CreativeAssets
            .Where(creativeAsset =>
                request.ChildIds.Contains(creativeAsset.Id) &&
                EF.Property<Guid?>(
                    creativeAsset,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    creativeAsset =>
                        EF.Property<Guid?>(
                            creativeAsset,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToPerformanceMetricsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PerformanceMetrics
            .Where(performanceMetric =>
                request.ChildIds.Contains(performanceMetric.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    performanceMetric =>
                        EF.Property<Guid?>(
                            performanceMetric,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPerformanceMetricsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PerformanceMetrics
            .Where(performanceMetric =>
                request.ChildIds.Contains(performanceMetric.Id) &&
                EF.Property<Guid?>(
                    performanceMetric,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    performanceMetric =>
                        EF.Property<Guid?>(
                            performanceMetric,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
