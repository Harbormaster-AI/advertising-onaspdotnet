using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IReportRepository
{
    Task<Report?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Report>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Report report, CancellationToken cancellationToken);
    Task UpdateAsync(Report report, CancellationToken cancellationToken);
    Task DeleteAsync(Report report, CancellationToken cancellationToken);


}
