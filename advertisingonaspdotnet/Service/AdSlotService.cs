using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IAdSlotService {

    Task Create(AdSlot model , CancellationToken cancellationToken);
    Task<bool> Update(AdSlot model, CancellationToken cancellationToken);
    Task<AdSlot?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AdSlot>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignInventorySource(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInventorySource(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRates(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AdSlotService : IAdSlotService
{
    private readonly IAdSlotRepository _repository;
    private readonly ILogger<AdSlotService> _logger;

    public AdSlotService(
        IAdSlotRepository repository, ILogger<AdSlotService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AdSlot model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(AdSlot model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.SlotCode = model.SlotCode;
            existing.Width = model.Width;
            existing.Height = model.Height;
            existing.FloorPrice = model.FloorPrice;
            existing.Format = model.Format;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<AdSlot?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AdSlot>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignInventorySource(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInventorySource(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToRates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromRates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
