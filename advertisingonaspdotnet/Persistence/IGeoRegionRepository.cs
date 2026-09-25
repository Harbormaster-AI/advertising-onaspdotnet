using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IGeoRegionRepository
{
    Task<GeoRegion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<GeoRegion>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(GeoRegion geoRegion, CancellationToken cancellationToken);
    Task UpdateAsync(GeoRegion geoRegion, CancellationToken cancellationToken);
    Task DeleteAsync(GeoRegion geoRegion, CancellationToken cancellationToken);

    Task AddToChildrenAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromChildrenAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
