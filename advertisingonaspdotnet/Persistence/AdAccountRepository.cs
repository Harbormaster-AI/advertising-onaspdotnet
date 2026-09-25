
using advertisingonaspdotnet.Contracts;
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


    public async Task AddToCampaignsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Campaigns
            .Where(campaign =>
                request.ChildIds.Contains(campaign.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    campaign =>
                        EF.Property<Guid?>(
                            campaign,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCampaignsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Campaigns
            .Where(campaign =>
                request.ChildIds.Contains(campaign.Id) &&
                EF.Property<Guid?>(
                    campaign,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    campaign =>
                        EF.Property<Guid?>(
                            campaign,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToPerformanceMetricsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PerformanceMetrics
            .Where(performanceMetric =>
                request.ChildIds.Contains(performanceMetric.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    performanceMetric =>
                        EF.Property<Guid?>(
                            performanceMetric,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPerformanceMetricsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PerformanceMetrics
            .Where(performanceMetric =>
                request.ChildIds.Contains(performanceMetric.Id) &&
                EF.Property<Guid?>(
                    performanceMetric,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    performanceMetric =>
                        EF.Property<Guid?>(
                            performanceMetric,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
