using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class InsertionOrderRepository : IInsertionOrderRepository
{
    private readonly ApplicationDbContext _db;

    public InsertionOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InsertionOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InsertionOrders
            .Include(x => x.Advertiser)
            .Include(x => x.Agency)
            .Include(x => x.Publisher)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InsertionOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InsertionOrders
            .AsNoTracking()
            .Include(x => x.Advertiser)
            .Include(x => x.Agency)
            .Include(x => x.Publisher)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InsertionOrder insertionOrder, CancellationToken cancellationToken)
    {
        _db.InsertionOrders.Add(insertionOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InsertionOrder insertionOrder, CancellationToken cancellationToken)
    {
        _db.InsertionOrders.Update(insertionOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InsertionOrder insertionOrder, CancellationToken cancellationToken)
    {
        _db.InsertionOrders.Remove(insertionOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
