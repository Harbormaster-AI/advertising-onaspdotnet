using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IContentCategoryRepository
{
    Task<ContentCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ContentCategory>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ContentCategory contentCategory, CancellationToken cancellationToken);
    Task UpdateAsync(ContentCategory contentCategory, CancellationToken cancellationToken);
    Task DeleteAsync(ContentCategory contentCategory, CancellationToken cancellationToken);


}
