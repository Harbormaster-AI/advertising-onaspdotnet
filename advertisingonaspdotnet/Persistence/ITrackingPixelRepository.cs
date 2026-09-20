using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface ITrackingPixelRepository
{
    Task<TrackingPixel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TrackingPixel>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TrackingPixel trackingPixel, CancellationToken cancellationToken);
    Task UpdateAsync(TrackingPixel trackingPixel, CancellationToken cancellationToken);
    Task DeleteAsync(TrackingPixel trackingPixel, CancellationToken cancellationToken);
}
