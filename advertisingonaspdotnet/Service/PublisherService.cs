using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IPublisherService {

    Task Create(Publisher model , CancellationToken cancellationToken);
    Task<bool> Update(Publisher model, CancellationToken cancellationToken);
    Task<Publisher?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Publisher>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToInventorySources(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInventorySources(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDeals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDeals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCreativeApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCreativeApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInsertionOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInsertionOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRateCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRateCards(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepository _repository;
    private readonly ILogger<PublisherService> _logger;

    public PublisherService(
        IPublisherRepository repository, ILogger<PublisherService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Publisher model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Publisher model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Website = model.Website;
            existing.PublisherType = model.PublisherType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Publisher?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Publisher>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToInventorySources(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInventorySources(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDeals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDeals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCreativeApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCreativeApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToInsertionOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInsertionOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToRateCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromRateCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
