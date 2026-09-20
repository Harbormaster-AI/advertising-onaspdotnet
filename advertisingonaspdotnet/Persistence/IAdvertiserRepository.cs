using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface IAdvertiserRepository
{
    Task<Advertiser?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Advertiser>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Advertiser advertiser, CancellationToken cancellationToken);
    Task UpdateAsync(Advertiser advertiser, CancellationToken cancellationToken);
    Task DeleteAsync(Advertiser advertiser, CancellationToken cancellationToken);
}
