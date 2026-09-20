using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IDeviceCriterionService {

    Task Create(DeviceCriterion model , CancellationToken cancellationToken);
    Task<bool> Update(DeviceCriterion model, CancellationToken cancellationToken);
    Task<DeviceCriterion?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceCriterion>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTargetingProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTargetingProfile(AssociationRequest request, CancellationToken cancellationToken);


}

public class DeviceCriterionService : IDeviceCriterionService
{
    private readonly IDeviceCriterionRepository _repository;
    private readonly ILogger<DeviceCriterionService> _logger;

    public DeviceCriterionService(
        IDeviceCriterionRepository repository, ILogger<DeviceCriterionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(DeviceCriterion model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(DeviceCriterion model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.DeviceType = model.DeviceType;
            existing.PlatformType = model.PlatformType;
            existing.Operator = model.Operator;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<DeviceCriterion?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DeviceCriterion>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignTargetingProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTargetingProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
