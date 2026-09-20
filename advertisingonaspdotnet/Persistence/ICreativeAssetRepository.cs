using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface ICreativeAssetRepository
{
    Task<CreativeAsset?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CreativeAsset>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken);
    Task UpdateAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken);
    Task DeleteAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken);
}
