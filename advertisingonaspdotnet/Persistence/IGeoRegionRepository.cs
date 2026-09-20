using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface IGeoRegionRepository
{
    Task<GeoRegion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<GeoRegion>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(GeoRegion geoRegion, CancellationToken cancellationToken);
    Task UpdateAsync(GeoRegion geoRegion, CancellationToken cancellationToken);
    Task DeleteAsync(GeoRegion geoRegion, CancellationToken cancellationToken);
}
