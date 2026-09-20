using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IDealService {

    Task Create(Deal model , CancellationToken cancellationToken);
    Task<bool> Update(Deal model, CancellationToken cancellationToken);
    Task<Deal?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Deal>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPublisher(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPublisher(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToInventorySources(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInventorySources(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DealService : IDealService
{
    private readonly IDealRepository _repository;
    private readonly ILogger<DealService> _logger;

    public DealService(
        IDealRepository repository, ILogger<DealService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Deal model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Deal model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.FloorPrice = model.FloorPrice;
            existing.DealType = model.DealType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Deal?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Deal>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPublisher(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPublisher(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToInventorySources(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInventorySources(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
