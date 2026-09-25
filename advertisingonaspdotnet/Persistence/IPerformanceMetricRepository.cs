using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IPerformanceMetricRepository
{
    Task<PerformanceMetric?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PerformanceMetric>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PerformanceMetric performanceMetric, CancellationToken cancellationToken);
    Task UpdateAsync(PerformanceMetric performanceMetric, CancellationToken cancellationToken);
    Task DeleteAsync(PerformanceMetric performanceMetric, CancellationToken cancellationToken);


}
