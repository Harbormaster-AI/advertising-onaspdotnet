using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface IPublisherRepository
{
    Task<Publisher?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Publisher>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Publisher publisher, CancellationToken cancellationToken);
    Task UpdateAsync(Publisher publisher, CancellationToken cancellationToken);
    Task DeleteAsync(Publisher publisher, CancellationToken cancellationToken);
}
