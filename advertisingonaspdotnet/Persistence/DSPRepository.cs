
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class DSPRepository : IDSPRepository
{
    private readonly ApplicationDbContext _db;

    public DSPRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DSP?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DSPs
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DSP>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DSPs
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DSP dSP, CancellationToken cancellationToken)
    {
        _db.DSPs.Add(dSP);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DSP dSP, CancellationToken cancellationToken)
    {
        _db.DSPs.Update(dSP);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DSP dSP, CancellationToken cancellationToken)
    {
        _db.DSPs.Remove(dSP);
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

}
