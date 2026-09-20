using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IInventorySourceService {

    Task Create(InventorySource model , CancellationToken cancellationToken);
    Task<bool> Update(InventorySource model, CancellationToken cancellationToken);
    Task<InventorySource?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InventorySource>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPublisher(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPublisher(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAdSlots(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAdSlots(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDeals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDeals(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InventorySourceService : IInventorySourceService
{
    private readonly IInventorySourceRepository _repository;
    private readonly ILogger<InventorySourceService> _logger;

    public InventorySourceService(
        IInventorySourceRepository repository, ILogger<InventorySourceService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(InventorySource model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(InventorySource model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Domain = model.Domain;
            existing.Channel = model.Channel;
            existing.PrimaryFormat = model.PrimaryFormat;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<InventorySource?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InventorySource>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToAdSlots(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAdSlots(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDeals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDeals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
