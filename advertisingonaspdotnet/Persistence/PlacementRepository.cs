
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class PlacementRepository : IPlacementRepository
{
    private readonly ApplicationDbContext _db;

    public PlacementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Placement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Placements
            .Include(x => x.LineItem)
            .Include(x => x.AdSlot)
            .Include(x => x.Deal)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Placement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Placements
            .AsNoTracking()
            .Include(x => x.LineItem)
            .Include(x => x.AdSlot)
            .Include(x => x.Deal)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Placement placement, CancellationToken cancellationToken)
    {
        _db.Placements.Add(placement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Placement placement, CancellationToken cancellationToken)
    {
        _db.Placements.Update(placement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Placement placement, CancellationToken cancellationToken)
    {
        _db.Placements.Remove(placement);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
