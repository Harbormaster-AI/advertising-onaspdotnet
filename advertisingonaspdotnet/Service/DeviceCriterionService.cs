
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IDeviceCriterionRepository _repository;
    private readonly ILogger<DeviceCriterionService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public DeviceCriterionService(
        ApplicationTelemetry telemetry,
        IDeviceCriterionRepository repository,
        ILogger<DeviceCriterionService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(DeviceCriterion model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "DeviceCriterion",
                "CreateDeviceCriterion",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            existing.Operator_ = model.Operator_;

            await _telemetry.Execute(
                "DeviceCriterion",
                "UpdateDeviceCriterion",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "DeviceCriterion",
                "UpdateDeviceCriterion",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignTargetingProfile(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DeviceCriterion found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<TargetingProfileService>().Get(childRequest, cancellationToken);
            parent.TargetingProfile = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignTargetingProfile(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DeviceCriterion found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.TargetingProfile = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }




}
