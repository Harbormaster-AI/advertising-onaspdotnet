using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class AdvertiserRepository : IAdvertiserRepository
{
    private readonly ApplicationDbContext _db;

    public AdvertiserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Advertiser?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Advertisers
            .Include(x => x.Agency)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Advertiser>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Advertisers
            .AsNoTracking()
            .Include(x => x.Agency)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Advertiser advertiser, CancellationToken cancellationToken)
    {
        _db.Advertisers.Add(advertiser);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Advertiser advertiser, CancellationToken cancellationToken)
    {
        _db.Advertisers.Update(advertiser);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Advertiser advertiser, CancellationToken cancellationToken)
    {
        _db.Advertisers.Remove(advertiser);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
