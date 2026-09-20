using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface ITargetingProfileRepository
{
    Task<TargetingProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TargetingProfile>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken);
    Task UpdateAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken);
    Task DeleteAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken);
}
