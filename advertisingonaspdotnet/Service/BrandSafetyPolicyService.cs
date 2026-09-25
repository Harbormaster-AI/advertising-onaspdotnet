
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Telemetry;

namespace advertisingonaspdotnet.Service;

public interface IBrandSafetyPolicyService {

    Task Create(BrandSafetyPolicy model , CancellationToken cancellationToken);
    Task<bool> Update(BrandSafetyPolicy model, CancellationToken cancellationToken);
    Task<BrandSafetyPolicy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BrandSafetyPolicy>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToTargetingProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTargetingProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class BrandSafetyPolicyService : IBrandSafetyPolicyService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IBrandSafetyPolicyRepository _repository;
    private readonly ILogger<BrandSafetyPolicyService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public BrandSafetyPolicyService(
        ApplicationTelemetry telemetry,
        IBrandSafetyPolicyRepository repository,
        ILogger<BrandSafetyPolicyService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(BrandSafetyPolicy model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "BrandSafetyPolicy",
                "CreateBrandSafetyPolicy",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(BrandSafetyPolicy model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Level = model.Level;
            existing.ContentRatingThreshold = model.ContentRatingThreshold;

            await _telemetry.Execute(
                "BrandSafetyPolicy",
                "UpdateBrandSafetyPolicy",
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

    public Task<BrandSafetyPolicy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BrandSafetyPolicy>> GetAll(CancellationToken cancellationToken)
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
                "BrandSafetyPolicy",
                "UpdateBrandSafetyPolicy",
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


    public async Task<bool> AddToTargetingProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BrandSafetyPolicy",
                "AddToTargetingProfiles",
                () => _repository.AddToTargetingProfilesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromTargetingProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BrandSafetyPolicy",
                "RemoveFromTargetingProfiles",
                () => _repository.RemoveFromTargetingProfilesAsync(request, cancellationToken));
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
