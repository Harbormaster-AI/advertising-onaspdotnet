using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class TeamRepository : ITeamRepository
{
    private readonly ApplicationDbContext _db;

    public TeamRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Team?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Teams
            .Include(x => x.Agency)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Team>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Teams
            .AsNoTracking()
            .Include(x => x.Agency)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Team team, CancellationToken cancellationToken)
    {
        _db.Teams.Add(team);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Team team, CancellationToken cancellationToken)
    {
        _db.Teams.Update(team);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Team team, CancellationToken cancellationToken)
    {
        _db.Teams.Remove(team);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
