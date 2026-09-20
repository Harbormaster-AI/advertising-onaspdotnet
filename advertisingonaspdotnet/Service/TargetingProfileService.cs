using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface ITargetingProfileService {

    Task Create(TargetingProfile model , CancellationToken cancellationToken);
    Task<bool> Update(TargetingProfile model, CancellationToken cancellationToken);
    Task<TargetingProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TargetingProfile>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignBrandSafetyPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBrandSafetyPolicy(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAudienceSegments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAudienceSegments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToGeoRegions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromGeoRegions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToContentCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromContentCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDeviceCriteria(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDeviceCriteria(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class TargetingProfileService : ITargetingProfileService
{
    private readonly ITargetingProfileRepository _repository;
    private readonly ILogger<TargetingProfileService> _logger;

    public TargetingProfileService(
        ITargetingProfileRepository repository, ILogger<TargetingProfileService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(TargetingProfile model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(TargetingProfile model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<TargetingProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TargetingProfile>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignBrandSafetyPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignBrandSafetyPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToAudienceSegments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAudienceSegments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToGeoRegions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromGeoRegions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToContentCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromContentCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDeviceCriteria(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDeviceCriteria(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
