
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class KPIRepository : IKPIRepository
{
    private readonly ApplicationDbContext _db;

    public KPIRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<KPI?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.KPIs
            .Include(x => x.Campaign)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<KPI>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.KPIs
            .AsNoTracking()
            .Include(x => x.Campaign)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(KPI kPI, CancellationToken cancellationToken)
    {
        _db.KPIs.Add(kPI);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(KPI kPI, CancellationToken cancellationToken)
    {
        _db.KPIs.Update(kPI);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(KPI kPI, CancellationToken cancellationToken)
    {
        _db.KPIs.Remove(kPI);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
