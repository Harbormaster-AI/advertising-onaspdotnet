using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class CreativeAssetRepository : ICreativeAssetRepository
{
    private readonly ApplicationDbContext _db;

    public CreativeAssetRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CreativeAsset?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CreativeAssets
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CreativeAsset>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CreativeAssets
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken)
    {
        _db.CreativeAssets.Add(creativeAsset);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken)
    {
        _db.CreativeAssets.Update(creativeAsset);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken)
    {
        _db.CreativeAssets.Remove(creativeAsset);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
