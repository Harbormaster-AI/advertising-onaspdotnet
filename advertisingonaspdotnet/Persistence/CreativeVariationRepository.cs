
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class CreativeVariationRepository : ICreativeVariationRepository
{
    private readonly ApplicationDbContext _db;

    public CreativeVariationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CreativeVariation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CreativeVariations
            .Include(x => x.CreativeAsset)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CreativeVariation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CreativeVariations
            .AsNoTracking()
            .Include(x => x.CreativeAsset)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CreativeVariation creativeVariation, CancellationToken cancellationToken)
    {
        _db.CreativeVariations.Add(creativeVariation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CreativeVariation creativeVariation, CancellationToken cancellationToken)
    {
        _db.CreativeVariations.Update(creativeVariation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CreativeVariation creativeVariation, CancellationToken cancellationToken)
    {
        _db.CreativeVariations.Remove(creativeVariation);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
