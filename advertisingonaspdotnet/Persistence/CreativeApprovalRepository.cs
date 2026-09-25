
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class CreativeApprovalRepository : ICreativeApprovalRepository
{
    private readonly ApplicationDbContext _db;

    public CreativeApprovalRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CreativeApproval?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CreativeApprovals
            .Include(x => x.CreativeAsset)
            .Include(x => x.Publisher)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CreativeApproval>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CreativeApprovals
            .AsNoTracking()
            .Include(x => x.CreativeAsset)
            .Include(x => x.Publisher)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CreativeApproval creativeApproval, CancellationToken cancellationToken)
    {
        _db.CreativeApprovals.Add(creativeApproval);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CreativeApproval creativeApproval, CancellationToken cancellationToken)
    {
        _db.CreativeApprovals.Update(creativeApproval);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CreativeApproval creativeApproval, CancellationToken cancellationToken)
    {
        _db.CreativeApprovals.Remove(creativeApproval);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
