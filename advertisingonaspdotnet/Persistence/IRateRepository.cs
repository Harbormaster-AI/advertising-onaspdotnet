using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IRateRepository
{
    Task<Rate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Rate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Rate rate, CancellationToken cancellationToken);
    Task UpdateAsync(Rate rate, CancellationToken cancellationToken);
    Task DeleteAsync(Rate rate, CancellationToken cancellationToken);


}
