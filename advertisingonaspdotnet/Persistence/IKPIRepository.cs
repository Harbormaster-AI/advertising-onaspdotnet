using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IKPIRepository
{
    Task<KPI?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<KPI>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(KPI kPI, CancellationToken cancellationToken);
    Task UpdateAsync(KPI kPI, CancellationToken cancellationToken);
    Task DeleteAsync(KPI kPI, CancellationToken cancellationToken);


}
