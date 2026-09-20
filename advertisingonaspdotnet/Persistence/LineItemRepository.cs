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
}
