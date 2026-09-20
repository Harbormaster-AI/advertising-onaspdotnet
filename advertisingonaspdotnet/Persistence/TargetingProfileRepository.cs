using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class TargetingProfileRepository : ITargetingProfileRepository
{
    private readonly ApplicationDbContext _db;

    public TargetingProfileRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TargetingProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TargetingProfiles
            .Include(x => x.BrandSafetyPolicy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TargetingProfile>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TargetingProfiles
            .AsNoTracking()
            .Include(x => x.BrandSafetyPolicy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken)
    {
        _db.TargetingProfiles.Add(targetingProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken)
    {
        _db.TargetingProfiles.Update(targetingProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken)
    {
        _db.TargetingProfiles.Remove(targetingProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
