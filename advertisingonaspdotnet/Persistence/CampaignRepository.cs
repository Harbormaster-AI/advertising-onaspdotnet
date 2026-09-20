using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class CampaignRepository : ICampaignRepository
{
    private readonly ApplicationDbContext _db;

    public CampaignRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Campaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Campaigns
            .Include(x => x.AdAccount)
            .Include(x => x.InsertionOrder)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Campaign>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Campaigns
            .AsNoTracking()
            .Include(x => x.AdAccount)
            .Include(x => x.InsertionOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        _db.Campaigns.Add(campaign);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        _db.Campaigns.Update(campaign);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        _db.Campaigns.Remove(campaign);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
