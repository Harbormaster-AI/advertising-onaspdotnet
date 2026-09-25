
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class PerformanceMetricRepository : IPerformanceMetricRepository
{
    private readonly ApplicationDbContext _db;

    public PerformanceMetricRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PerformanceMetric?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PerformanceMetrics
            .Include(x => x.AdAccount)
            .Include(x => x.Campaign)
            .Include(x => x.LineItem)
            .Include(x => x.Placement)
            .Include(x => x.CreativeAsset)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PerformanceMetric>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PerformanceMetrics
            .AsNoTracking()
            .Include(x => x.AdAccount)
            .Include(x => x.Campaign)
            .Include(x => x.LineItem)
            .Include(x => x.Placement)
            .Include(x => x.CreativeAsset)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PerformanceMetric performanceMetric, CancellationToken cancellationToken)
    {
        _db.PerformanceMetrics.Add(performanceMetric);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PerformanceMetric performanceMetric, CancellationToken cancellationToken)
    {
        _db.PerformanceMetrics.Update(performanceMetric);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PerformanceMetric performanceMetric, CancellationToken cancellationToken)
    {
        _db.PerformanceMetrics.Remove(performanceMetric);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
