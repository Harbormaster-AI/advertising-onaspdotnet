
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPublisherRepository _repository;
    private readonly ILogger<PublisherService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PublisherService(
        ApplicationTelemetry telemetry,
        IPublisherRepository repository,
        ILogger<PublisherService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Publisher model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Publisher",
                "CreatePublisher",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "Publisher",
                "UpdatePublisher",
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
            await _telemetry.Execute(
                "Publisher",
                "UpdatePublisher",
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


    public async Task<bool> AddToInventorySources(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Publisher",
                "AddToInventorySources",
                () => _repository.AddToInventorySourcesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromInventorySources(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Publisher",
                "RemoveFromInventorySources",
                () => _repository.RemoveFromInventorySourcesAsync(request, cancellationToken));
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

    public async Task<bool> AddToDeals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Publisher",
                "AddToDeals",
                () => _repository.AddToDealsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDeals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Publisher",
                "RemoveFromDeals",
                () => _repository.RemoveFromDealsAsync(request, cancellationToken));
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

    public async Task<bool> AddToCreativeApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Publisher",
                "AddToCreativeApprovals",
                () => _repository.AddToCreativeApprovalsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCreativeApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Publisher",
                "RemoveFromCreativeApprovals",
                () => _repository.RemoveFromCreativeApprovalsAsync(request, cancellationToken));
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

    public async Task<bool> AddToInsertionOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Publisher",
                "AddToInsertionOrders",
                () => _repository.AddToInsertionOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromInsertionOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Publisher",
                "RemoveFromInsertionOrders",
                () => _repository.RemoveFromInsertionOrdersAsync(request, cancellationToken));
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

    public async Task<bool> AddToRateCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Publisher",
                "AddToRateCards",
                () => _repository.AddToRateCardsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRateCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Publisher",
                "RemoveFromRateCards",
                () => _repository.RemoveFromRateCardsAsync(request, cancellationToken));
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
