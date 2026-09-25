
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Telemetry;

namespace advertisingonaspdotnet.Service;

public interface ICreativeFileService {

    Task Create(CreativeFile model , CancellationToken cancellationToken);
    Task<bool> Update(CreativeFile model, CancellationToken cancellationToken);
    Task<CreativeFile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CreativeFile>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken);


}

public class CreativeFileService : ICreativeFileService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICreativeFileRepository _repository;
    private readonly ILogger<CreativeFileService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CreativeFileService(
        ApplicationTelemetry telemetry,
        ICreativeFileRepository repository,
        ILogger<CreativeFileService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(CreativeFile model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "CreativeFile",
                "CreateCreativeFile",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(CreativeFile model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Uri = model.Uri;
            existing.FileSizeKB = model.FileSizeKB;
            existing.MimeType = model.MimeType;
            existing.Checksum = model.Checksum;

            await _telemetry.Execute(
                "CreativeFile",
                "UpdateCreativeFile",
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

    public Task<CreativeFile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CreativeFile>> GetAll(CancellationToken cancellationToken)
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
                "CreativeFile",
                "UpdateCreativeFile",
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
            _logger.LogError("No CreativeFile found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No CreativeFile found using Id {ParentId}", request.ParentId);
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
