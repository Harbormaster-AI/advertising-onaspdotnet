using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class AgencyRepository : IAgencyRepository
{
    private readonly ApplicationDbContext _db;

    public AgencyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Agency?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Agencys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Agency>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Agencys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Agency agency, CancellationToken cancellationToken)
    {
        _db.Agencys.Add(agency);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Agency agency, CancellationToken cancellationToken)
    {
        _db.Agencys.Update(agency);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Agency agency, CancellationToken cancellationToken)
    {
        _db.Agencys.Remove(agency);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
