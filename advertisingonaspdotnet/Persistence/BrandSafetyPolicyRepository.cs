
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class BrandSafetyPolicyRepository : IBrandSafetyPolicyRepository
{
    private readonly ApplicationDbContext _db;

    public BrandSafetyPolicyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BrandSafetyPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BrandSafetyPolicys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BrandSafetyPolicy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BrandSafetyPolicys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BrandSafetyPolicy brandSafetyPolicy, CancellationToken cancellationToken)
    {
        _db.BrandSafetyPolicys.Add(brandSafetyPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BrandSafetyPolicy brandSafetyPolicy, CancellationToken cancellationToken)
    {
        _db.BrandSafetyPolicys.Update(brandSafetyPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BrandSafetyPolicy brandSafetyPolicy, CancellationToken cancellationToken)
    {
        _db.BrandSafetyPolicys.Remove(brandSafetyPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToTargetingProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TargetingProfiles
            .Where(targetingProfile =>
                request.ChildIds.Contains(targetingProfile.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    targetingProfile =>
                        EF.Property<Guid?>(
                            targetingProfile,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTargetingProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TargetingProfiles
            .Where(targetingProfile =>
                request.ChildIds.Contains(targetingProfile.Id) &&
                EF.Property<Guid?>(
                    targetingProfile,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    targetingProfile =>
                        EF.Property<Guid?>(
                            targetingProfile,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
