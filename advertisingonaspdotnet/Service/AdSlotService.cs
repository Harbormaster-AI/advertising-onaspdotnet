
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IAdSlotRepository _repository;
    private readonly ILogger<AdSlotService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public AdSlotService(
        ApplicationTelemetry telemetry,
        IAdSlotRepository repository,
        ILogger<AdSlotService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(AdSlot model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "AdSlot",
                "CreateAdSlot",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "AdSlot",
                "UpdateAdSlot",
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
            await _telemetry.Execute(
                "AdSlot",
                "UpdateAdSlot",
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

    public async Task<bool> AssignInventorySource(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AdSlot found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<InventorySourceService>().Get(childRequest, cancellationToken);
            parent.InventorySource = child;
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

    public async Task<bool> UnassignInventorySource(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AdSlot found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.InventorySource = null;
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


    public async Task<bool> AddToPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AdSlot",
                "AddToPlacements",
                () => _repository.AddToPlacementsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AdSlot",
                "RemoveFromPlacements",
                () => _repository.RemoveFromPlacementsAsync(request, cancellationToken));
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

    public async Task<bool> AddToRates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AdSlot",
                "AddToRates",
                () => _repository.AddToRatesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AdSlot",
                "RemoveFromRates",
                () => _repository.RemoveFromRatesAsync(request, cancellationToken));
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
