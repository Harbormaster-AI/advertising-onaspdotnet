using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IDataProviderService {

    Task Create(DataProvider model , CancellationToken cancellationToken);
    Task<bool> Update(DataProvider model, CancellationToken cancellationToken);
    Task<DataProvider?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataProvider>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToAudienceSegments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAudienceSegments(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DataProviderService : IDataProviderService
{
    private readonly IDataProviderRepository _repository;
    private readonly ILogger<DataProviderService> _logger;

    public DataProviderService(
        IDataProviderRepository repository, ILogger<DataProviderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(DataProvider model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(DataProvider model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Website = model.Website;
            existing.ProviderType = model.ProviderType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<DataProvider?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DataProvider>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToAudienceSegments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAudienceSegments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
