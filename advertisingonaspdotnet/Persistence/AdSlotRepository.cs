
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class AdSlotRepository : IAdSlotRepository
{
    private readonly ApplicationDbContext _db;

    public AdSlotRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AdSlot?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AdSlots
            .Include(x => x.InventorySource)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AdSlot>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AdSlots
            .AsNoTracking()
            .Include(x => x.InventorySource)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AdSlot adSlot, CancellationToken cancellationToken)
    {
        _db.AdSlots.Add(adSlot);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AdSlot adSlot, CancellationToken cancellationToken)
    {
        _db.AdSlots.Update(adSlot);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AdSlot adSlot, CancellationToken cancellationToken)
    {
        _db.AdSlots.Remove(adSlot);
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


    public async Task AddToRatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Rates
            .Where(rate =>
                request.ChildIds.Contains(rate.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    rate =>
                        EF.Property<Guid?>(
                            rate,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Rates
            .Where(rate =>
                request.ChildIds.Contains(rate.Id) &&
                EF.Property<Guid?>(
                    rate,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    rate =>
                        EF.Property<Guid?>(
                            rate,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
