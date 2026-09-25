using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IExperimentRepository
{
    Task<Experiment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Experiment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Experiment experiment, CancellationToken cancellationToken);
    Task UpdateAsync(Experiment experiment, CancellationToken cancellationToken);
    Task DeleteAsync(Experiment experiment, CancellationToken cancellationToken);

    Task AddToVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
