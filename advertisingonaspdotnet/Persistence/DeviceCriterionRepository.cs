
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class DeviceCriterionRepository : IDeviceCriterionRepository
{
    private readonly ApplicationDbContext _db;

    public DeviceCriterionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DeviceCriterion?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DeviceCriterions
            .Include(x => x.TargetingProfile)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DeviceCriterion>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DeviceCriterions
            .AsNoTracking()
            .Include(x => x.TargetingProfile)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DeviceCriterion deviceCriterion, CancellationToken cancellationToken)
    {
        _db.DeviceCriterions.Add(deviceCriterion);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DeviceCriterion deviceCriterion, CancellationToken cancellationToken)
    {
        _db.DeviceCriterions.Update(deviceCriterion);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeviceCriterion deviceCriterion, CancellationToken cancellationToken)
    {
        _db.DeviceCriterions.Remove(deviceCriterion);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
