
using advertisingonaspdotnet.Contracts;
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


    public async Task AddToUsersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Users
            .Where(user =>
                request.ChildIds.Contains(user.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    user =>
                        EF.Property<Guid?>(
                            user,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromUsersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Users
            .Where(user =>
                request.ChildIds.Contains(user.Id) &&
                EF.Property<Guid?>(
                    user,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    user =>
                        EF.Property<Guid?>(
                            user,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToAdAccountsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AdAccounts
            .Where(adAccount =>
                request.ChildIds.Contains(adAccount.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    adAccount =>
                        EF.Property<Guid?>(
                            adAccount,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAdAccountsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AdAccounts
            .Where(adAccount =>
                request.ChildIds.Contains(adAccount.Id) &&
                EF.Property<Guid?>(
                    adAccount,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    adAccount =>
                        EF.Property<Guid?>(
                            adAccount,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
