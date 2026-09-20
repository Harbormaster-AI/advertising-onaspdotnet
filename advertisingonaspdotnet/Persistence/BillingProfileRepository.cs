using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class BillingProfileRepository : IBillingProfileRepository
{
    private readonly ApplicationDbContext _db;

    public BillingProfileRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BillingProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BillingProfiles
            .Include(x => x.Advertiser)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BillingProfile>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BillingProfiles
            .AsNoTracking()
            .Include(x => x.Advertiser)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BillingProfile billingProfile, CancellationToken cancellationToken)
    {
        _db.BillingProfiles.Add(billingProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BillingProfile billingProfile, CancellationToken cancellationToken)
    {
        _db.BillingProfiles.Update(billingProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BillingProfile billingProfile, CancellationToken cancellationToken)
    {
        _db.BillingProfiles.Remove(billingProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
