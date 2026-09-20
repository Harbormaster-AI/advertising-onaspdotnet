using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IConversionEventService {

    Task Create(ConversionEvent model , CancellationToken cancellationToken);
    Task<bool> Update(ConversionEvent model, CancellationToken cancellationToken);
    Task<ConversionEvent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConversionEvent>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCampaign(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCampaign(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLineItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLineItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTrackingPixel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTrackingPixel(AssociationRequest request, CancellationToken cancellationToken);


}

public class ConversionEventService : IConversionEventService
{
    private readonly IConversionEventRepository _repository;
    private readonly ILogger<ConversionEventService> _logger;

    public ConversionEventService(
        IConversionEventRepository repository, ILogger<ConversionEventService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ConversionEvent model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ConversionEvent model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Timestamp = model.Timestamp;
            existing.Value = model.Value;
            existing.EventType = model.EventType;
            existing.AttributionModel = model.AttributionModel;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ConversionEvent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ConversionEvent>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignLineItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLineItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTrackingPixel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTrackingPixel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
