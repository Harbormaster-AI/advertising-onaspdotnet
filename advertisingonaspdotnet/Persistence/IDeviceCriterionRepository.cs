using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IDeviceCriterionRepository
{
    Task<DeviceCriterion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceCriterion>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DeviceCriterion deviceCriterion, CancellationToken cancellationToken);
    Task UpdateAsync(DeviceCriterion deviceCriterion, CancellationToken cancellationToken);
    Task DeleteAsync(DeviceCriterion deviceCriterion, CancellationToken cancellationToken);


}
