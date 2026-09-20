using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IPerformanceMetricService {

    Task Create(PerformanceMetric model , CancellationToken cancellationToken);
    Task<bool> Update(PerformanceMetric model, CancellationToken cancellationToken);
    Task<PerformanceMetric?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PerformanceMetric>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAdAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAdAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCampaign(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCampaign(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLineItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLineItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPlacement(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPlacement(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken);


}

public class PerformanceMetricService : IPerformanceMetricService
{
    private readonly IPerformanceMetricRepository _repository;
    private readonly ILogger<PerformanceMetricService> _logger;

    public PerformanceMetricService(
        IPerformanceMetricRepository repository, ILogger<PerformanceMetricService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PerformanceMetric model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PerformanceMetric model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Date = model.Date;
            existing.Value = model.Value;
            existing.MetricType = model.MetricType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<PerformanceMetric?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PerformanceMetric>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignAdAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAdAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCampaign(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCampaign(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignLineItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLineItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPlacement(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPlacement(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
