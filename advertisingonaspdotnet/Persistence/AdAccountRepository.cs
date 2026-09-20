using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class AdAccountRepository : IAdAccountRepository
{
    private readonly ApplicationDbContext _db;

    public AdAccountRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AdAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AdAccounts
            .Include(x => x.Advertiser)
            .Include(x => x.BillingProfile)
            .Include(x => x.Dsp)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AdAccount>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AdAccounts
            .AsNoTracking()
            .Include(x => x.Advertiser)
            .Include(x => x.BillingProfile)
            .Include(x => x.Dsp)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AdAccount adAccount, CancellationToken cancellationToken)
    {
        _db.AdAccounts.Add(adAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AdAccount adAccount, CancellationToken cancellationToken)
    {
        _db.AdAccounts.Update(adAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AdAccount adAccount, CancellationToken cancellationToken)
    {
        _db.AdAccounts.Remove(adAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
