using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IExperimentVariantRepository
{
    Task<ExperimentVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ExperimentVariant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ExperimentVariant experimentVariant, CancellationToken cancellationToken);
    Task UpdateAsync(ExperimentVariant experimentVariant, CancellationToken cancellationToken);
    Task DeleteAsync(ExperimentVariant experimentVariant, CancellationToken cancellationToken);


}
