using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface IBrandSafetyPolicyRepository
{
    Task<BrandSafetyPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BrandSafetyPolicy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BrandSafetyPolicy brandSafetyPolicy, CancellationToken cancellationToken);
    Task UpdateAsync(BrandSafetyPolicy brandSafetyPolicy, CancellationToken cancellationToken);
    Task DeleteAsync(BrandSafetyPolicy brandSafetyPolicy, CancellationToken cancellationToken);
}
