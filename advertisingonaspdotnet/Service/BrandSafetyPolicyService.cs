using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

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
    private readonly IBrandSafetyPolicyRepository _repository;
    private readonly ILogger<BrandSafetyPolicyService> _logger;

    public BrandSafetyPolicyService(
        IBrandSafetyPolicyRepository repository, ILogger<BrandSafetyPolicyService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(BrandSafetyPolicy model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }


    public async Task<bool> AddToTargetingProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTargetingProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
