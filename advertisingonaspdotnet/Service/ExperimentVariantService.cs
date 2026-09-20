using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IExperimentVariantService {

    Task Create(ExperimentVariant model , CancellationToken cancellationToken);
    Task<bool> Update(ExperimentVariant model, CancellationToken cancellationToken);
    Task<ExperimentVariant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ExperimentVariant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignExperiment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignExperiment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCreativeVariation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCreativeVariation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLineItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLineItem(AssociationRequest request, CancellationToken cancellationToken);


}

public class ExperimentVariantService : IExperimentVariantService
{
    private readonly IExperimentVariantRepository _repository;
    private readonly ILogger<ExperimentVariantService> _logger;

    public ExperimentVariantService(
        IExperimentVariantRepository repository, ILogger<ExperimentVariantService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ExperimentVariant model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ExperimentVariant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Allocation = model.Allocation;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ExperimentVariant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ExperimentVariant>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignExperiment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignExperiment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCreativeVariation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCreativeVariation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignLineItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLineItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
