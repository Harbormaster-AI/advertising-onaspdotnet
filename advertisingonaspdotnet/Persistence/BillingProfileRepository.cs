
using advertisingonaspdotnet.Contracts;
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


    public async Task AddToPaymentMethodsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentMethods
            .Where(paymentMethod =>
                request.ChildIds.Contains(paymentMethod.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentMethod =>
                        EF.Property<Guid?>(
                            paymentMethod,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPaymentMethodsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentMethods
            .Where(paymentMethod =>
                request.ChildIds.Contains(paymentMethod.Id) &&
                EF.Property<Guid?>(
                    paymentMethod,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentMethod =>
                        EF.Property<Guid?>(
                            paymentMethod,
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
