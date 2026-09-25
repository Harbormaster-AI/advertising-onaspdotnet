
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly ITargetingProfileRepository _repository;
    private readonly ILogger<TargetingProfileService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public TargetingProfileService(
        ApplicationTelemetry telemetry,
        ITargetingProfileRepository repository,
        ILogger<TargetingProfileService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(TargetingProfile model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "TargetingProfile",
                "CreateTargetingProfile",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "TargetingProfile",
                "UpdateTargetingProfile",
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
            await _telemetry.Execute(
                "TargetingProfile",
                "UpdateTargetingProfile",
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

    public async Task<bool> AssignBrandSafetyPolicy(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No TargetingProfile found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<BrandSafetyPolicyService>().Get(childRequest, cancellationToken);
            parent.BrandSafetyPolicy = child;
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

    public async Task<bool> UnassignBrandSafetyPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No TargetingProfile found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.BrandSafetyPolicy = null;
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


    public async Task<bool> AddToAudienceSegments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TargetingProfile",
                "AddToAudienceSegments",
                () => _repository.AddToAudienceSegmentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAudienceSegments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TargetingProfile",
                "RemoveFromAudienceSegments",
                () => _repository.RemoveFromAudienceSegmentsAsync(request, cancellationToken));
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

    public async Task<bool> AddToGeoRegions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TargetingProfile",
                "AddToGeoRegions",
                () => _repository.AddToGeoRegionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromGeoRegions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TargetingProfile",
                "RemoveFromGeoRegions",
                () => _repository.RemoveFromGeoRegionsAsync(request, cancellationToken));
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

    public async Task<bool> AddToContentCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TargetingProfile",
                "AddToContentCategories",
                () => _repository.AddToContentCategoriesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromContentCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TargetingProfile",
                "RemoveFromContentCategories",
                () => _repository.RemoveFromContentCategoriesAsync(request, cancellationToken));
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

    public async Task<bool> AddToDeviceCriteria(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TargetingProfile",
                "AddToDeviceCriteria",
                () => _repository.AddToDeviceCriteriaAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDeviceCriteria(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TargetingProfile",
                "RemoveFromDeviceCriteria",
                () => _repository.RemoveFromDeviceCriteriaAsync(request, cancellationToken));
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
