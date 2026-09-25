using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface ICreativeFileRepository
{
    Task<CreativeFile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CreativeFile>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CreativeFile creativeFile, CancellationToken cancellationToken);
    Task UpdateAsync(CreativeFile creativeFile, CancellationToken cancellationToken);
    Task DeleteAsync(CreativeFile creativeFile, CancellationToken cancellationToken);


}
