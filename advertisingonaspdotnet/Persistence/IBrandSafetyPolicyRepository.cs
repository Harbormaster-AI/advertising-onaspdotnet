using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IBrandSafetyPolicyRepository
{
    Task<BrandSafetyPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BrandSafetyPolicy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BrandSafetyPolicy brandSafetyPolicy, CancellationToken cancellationToken);
    Task UpdateAsync(BrandSafetyPolicy brandSafetyPolicy, CancellationToken cancellationToken);
    Task DeleteAsync(BrandSafetyPolicy brandSafetyPolicy, CancellationToken cancellationToken);

    Task AddToTargetingProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTargetingProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
