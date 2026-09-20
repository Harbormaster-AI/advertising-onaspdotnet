using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class InventorySourceRepository : IInventorySourceRepository
{
    private readonly ApplicationDbContext _db;

    public InventorySourceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InventorySource?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InventorySources
            .Include(x => x.Publisher)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InventorySource>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InventorySources
            .AsNoTracking()
            .Include(x => x.Publisher)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InventorySource inventorySource, CancellationToken cancellationToken)
    {
        _db.InventorySources.Add(inventorySource);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InventorySource inventorySource, CancellationToken cancellationToken)
    {
        _db.InventorySources.Update(inventorySource);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InventorySource inventorySource, CancellationToken cancellationToken)
    {
        _db.InventorySources.Remove(inventorySource);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
