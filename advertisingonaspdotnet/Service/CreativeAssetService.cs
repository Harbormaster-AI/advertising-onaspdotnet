
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Telemetry;

namespace advertisingonaspdotnet.Service;

public interface ICreativeAssetService {

    Task Create(CreativeAsset model , CancellationToken cancellationToken);
    Task<bool> Update(CreativeAsset model, CancellationToken cancellationToken);
    Task<CreativeAsset?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CreativeAsset>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToFiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToVariations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromVariations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLineItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLineItems(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CreativeAssetService : ICreativeAssetService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICreativeAssetRepository _repository;
    private readonly ILogger<CreativeAssetService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CreativeAssetService(
        ApplicationTelemetry telemetry,
        ICreativeAssetRepository repository,
        ILogger<CreativeAssetService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(CreativeAsset model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "CreativeAsset",
                "CreateCreativeAsset",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(CreativeAsset model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.ClickUrl = model.ClickUrl;
            existing.LandingPage = model.LandingPage;
            existing.Width = model.Width;
            existing.Height = model.Height;
            existing.DurationSeconds = model.DurationSeconds;
            existing.CreativeType = model.CreativeType;
            existing.AdFormat = model.AdFormat;

            await _telemetry.Execute(
                "CreativeAsset",
                "UpdateCreativeAsset",
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

    public Task<CreativeAsset?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CreativeAsset>> GetAll(CancellationToken cancellationToken)
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
                "CreativeAsset",
                "UpdateCreativeAsset",
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


    public async Task<bool> AddToFiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CreativeAsset",
                "AddToFiles",
                () => _repository.AddToFilesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromFiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CreativeAsset",
                "RemoveFromFiles",
                () => _repository.RemoveFromFilesAsync(request, cancellationToken));
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

    public async Task<bool> AddToApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CreativeAsset",
                "AddToApprovals",
                () => _repository.AddToApprovalsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CreativeAsset",
                "RemoveFromApprovals",
                () => _repository.RemoveFromApprovalsAsync(request, cancellationToken));
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

    public async Task<bool> AddToVariations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CreativeAsset",
                "AddToVariations",
                () => _repository.AddToVariationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromVariations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CreativeAsset",
                "RemoveFromVariations",
                () => _repository.RemoveFromVariationsAsync(request, cancellationToken));
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

    public async Task<bool> AddToLineItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CreativeAsset",
                "AddToLineItems",
                () => _repository.AddToLineItemsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromLineItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CreativeAsset",
                "RemoveFromLineItems",
                () => _repository.RemoveFromLineItemsAsync(request, cancellationToken));
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
