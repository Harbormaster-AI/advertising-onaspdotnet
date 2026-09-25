
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class RateCardRepository : IRateCardRepository
{
    private readonly ApplicationDbContext _db;

    public RateCardRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<RateCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.RateCards
            .Include(x => x.Publisher)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<RateCard>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.RateCards
            .AsNoTracking()
            .Include(x => x.Publisher)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RateCard rateCard, CancellationToken cancellationToken)
    {
        _db.RateCards.Add(rateCard);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RateCard rateCard, CancellationToken cancellationToken)
    {
        _db.RateCards.Update(rateCard);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RateCard rateCard, CancellationToken cancellationToken)
    {
        _db.RateCards.Remove(rateCard);
        await _db.SaveChangesAsync(cancellationToken);
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
