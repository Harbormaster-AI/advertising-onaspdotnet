using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class DealRepository : IDealRepository
{
    private readonly ApplicationDbContext _db;

    public DealRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Deal?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Deals
            .Include(x => x.Publisher)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Deal>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Deals
            .AsNoTracking()
            .Include(x => x.Publisher)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Deal deal, CancellationToken cancellationToken)
    {
        _db.Deals.Add(deal);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Deal deal, CancellationToken cancellationToken)
    {
        _db.Deals.Update(deal);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Deal deal, CancellationToken cancellationToken)
    {
        _db.Deals.Remove(deal);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
