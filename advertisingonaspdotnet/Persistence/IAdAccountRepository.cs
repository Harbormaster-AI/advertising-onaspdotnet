using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface IAdAccountRepository
{
    Task<AdAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AdAccount>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AdAccount adAccount, CancellationToken cancellationToken);
    Task UpdateAsync(AdAccount adAccount, CancellationToken cancellationToken);
    Task DeleteAsync(AdAccount adAccount, CancellationToken cancellationToken);
}
