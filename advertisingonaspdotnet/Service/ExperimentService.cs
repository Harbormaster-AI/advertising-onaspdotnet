using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IExperimentService {

    Task Create(Experiment model , CancellationToken cancellationToken);
    Task<bool> Update(Experiment model, CancellationToken cancellationToken);
    Task<Experiment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Experiment>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCampaign(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCampaign(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToVariants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromVariants(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ExperimentService : IExperimentService
{
    private readonly IExperimentRepository _repository;
    private readonly ILogger<ExperimentService> _logger;

    public ExperimentService(
        IExperimentRepository repository, ILogger<ExperimentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Experiment model, CancellationToken cancellationToken)
    {

         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(Experiment model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Hypothesis = model.Hypothesis;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Experiment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Experiment>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignCampaign(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCampaign(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToVariants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromVariants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
