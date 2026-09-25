
using advertisingonaspdotnet.Contracts;
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


    public async Task AddToAdvertisersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Advertisers
            .Where(advertiser =>
                request.ChildIds.Contains(advertiser.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    advertiser =>
                        EF.Property<Guid?>(
                            advertiser,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAdvertisersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Advertisers
            .Where(advertiser =>
                request.ChildIds.Contains(advertiser.Id) &&
                EF.Property<Guid?>(
                    advertiser,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    advertiser =>
                        EF.Property<Guid?>(
                            advertiser,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToTeamsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Teams
            .Where(team =>
                request.ChildIds.Contains(team.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    team =>
                        EF.Property<Guid?>(
                            team,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTeamsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Teams
            .Where(team =>
                request.ChildIds.Contains(team.Id) &&
                EF.Property<Guid?>(
                    team,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    team =>
                        EF.Property<Guid?>(
                            team,
                            "GeoRegion_Id"),
                    (Guid?)null));
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


    public async Task AddToInsertionOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InsertionOrders
            .Where(insertionOrder =>
                request.ChildIds.Contains(insertionOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    insertionOrder =>
                        EF.Property<Guid?>(
                            insertionOrder,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInsertionOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InsertionOrders
            .Where(insertionOrder =>
                request.ChildIds.Contains(insertionOrder.Id) &&
                EF.Property<Guid?>(
                    insertionOrder,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    insertionOrder =>
                        EF.Property<Guid?>(
                            insertionOrder,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
