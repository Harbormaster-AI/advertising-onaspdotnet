
using advertisingonaspdotnet.Contracts;
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


    public async Task AddToBillingProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BillingProfiles
            .Where(billingProfile =>
                request.ChildIds.Contains(billingProfile.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    billingProfile =>
                        EF.Property<Guid?>(
                            billingProfile,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBillingProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BillingProfiles
            .Where(billingProfile =>
                request.ChildIds.Contains(billingProfile.Id) &&
                EF.Property<Guid?>(
                    billingProfile,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    billingProfile =>
                        EF.Property<Guid?>(
                            billingProfile,
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


    public async Task AddToTrackingPixelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TrackingPixels
            .Where(trackingPixel =>
                request.ChildIds.Contains(trackingPixel.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    trackingPixel =>
                        EF.Property<Guid?>(
                            trackingPixel,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTrackingPixelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TrackingPixels
            .Where(trackingPixel =>
                request.ChildIds.Contains(trackingPixel.Id) &&
                EF.Property<Guid?>(
                    trackingPixel,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    trackingPixel =>
                        EF.Property<Guid?>(
                            trackingPixel,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
