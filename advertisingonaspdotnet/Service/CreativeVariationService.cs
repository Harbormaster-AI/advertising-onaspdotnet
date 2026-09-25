
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Telemetry;

namespace advertisingonaspdotnet.Service;

public interface ICreativeVariationService {

    Task Create(CreativeVariation model , CancellationToken cancellationToken);
    Task<bool> Update(CreativeVariation model, CancellationToken cancellationToken);
    Task<CreativeVariation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CreativeVariation>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken);


}

public class CreativeVariationService : ICreativeVariationService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICreativeVariationRepository _repository;
    private readonly ILogger<CreativeVariationService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CreativeVariationService(
        ApplicationTelemetry telemetry,
        ICreativeVariationRepository repository,
        ILogger<CreativeVariationService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(CreativeVariation model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "CreativeVariation",
                "CreateCreativeVariation",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(CreativeVariation model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Language = model.Language;
            existing.Headline = model.Headline;
            existing.BodyText = model.BodyText;
            existing.CallToAction = model.CallToAction;

            await _telemetry.Execute(
                "CreativeVariation",
                "UpdateCreativeVariation",
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

    public Task<CreativeVariation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CreativeVariation>> GetAll(CancellationToken cancellationToken)
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
                "CreativeVariation",
                "UpdateCreativeVariation",
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

    public async Task<bool> AssignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CreativeVariation found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<CreativeAssetService>().Get(childRequest, cancellationToken);
            parent.CreativeAsset = child;
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

    public async Task<bool> UnassignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CreativeVariation found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.CreativeAsset = null;
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
