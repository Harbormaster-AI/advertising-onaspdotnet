using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

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
    private readonly ICreativeFileRepository _repository;
    private readonly ILogger<CreativeFileService> _logger;

    public CreativeFileService(
        ICreativeFileRepository repository, ILogger<CreativeFileService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CreativeFile model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
