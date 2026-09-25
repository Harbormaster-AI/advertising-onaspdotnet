
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class RateRepository : IRateRepository
{
    private readonly ApplicationDbContext _db;

    public RateRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Rate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Rates
            .Include(x => x.RateCard)
            .Include(x => x.AdSlot)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Rate>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Rates
            .AsNoTracking()
            .Include(x => x.RateCard)
            .Include(x => x.AdSlot)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Rate rate, CancellationToken cancellationToken)
    {
        _db.Rates.Add(rate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Rate rate, CancellationToken cancellationToken)
    {
        _db.Rates.Update(rate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Rate rate, CancellationToken cancellationToken)
    {
        _db.Rates.Remove(rate);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
