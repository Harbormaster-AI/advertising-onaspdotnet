using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface ILineItemService {

    Task Create(LineItem model , CancellationToken cancellationToken);
    Task<bool> Update(LineItem model, CancellationToken cancellationToken);
    Task<LineItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<LineItem>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCampaign(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCampaign(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTargetingProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTargetingProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDeal(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDeal(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCreatives(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCreatives(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPerformanceMetrics(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPerformanceMetrics(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class LineItemService : ILineItemService
{
    private readonly ILineItemRepository _repository;
    private readonly ILogger<LineItemService> _logger;

    public LineItemService(
        ILineItemRepository repository, ILogger<LineItemService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(LineItem model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(LineItem model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.BidAmount = model.BidAmount;
            existing.DailyBudget = model.DailyBudget;
            existing.FrequencyCap = model.FrequencyCap;
            existing.Status = model.Status;
            existing.PricingModel = model.PricingModel;
            existing.BidStrategy = model.BidStrategy;
            existing.Pacing = model.Pacing;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<LineItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<LineItem>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCampaign(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCampaign(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTargetingProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTargetingProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignDeal(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDeal(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPlacements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCreatives(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCreatives(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPerformanceMetrics(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPerformanceMetrics(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
