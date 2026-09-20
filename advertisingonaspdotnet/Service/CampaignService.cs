using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface ICampaignService {

    Task Create(Campaign model , CancellationToken cancellationToken);
    Task<bool> Update(Campaign model, CancellationToken cancellationToken);
    Task<Campaign?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Campaign>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAdAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAdAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignInsertionOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInsertionOrder(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToLineItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLineItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToKpis(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromKpis(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTrackingPixels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTrackingPixels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAudiences(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAudiences(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToReports(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromReports(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _repository;
    private readonly ILogger<CampaignService> _logger;

    public CampaignService(
        ICampaignRepository repository, ILogger<CampaignService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Campaign model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Campaign model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.TotalBudget = model.TotalBudget;
            existing.Flight = model.Flight;
            existing.Objective = model.Objective;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Campaign?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Campaign>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignAdAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAdAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignInsertionOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInsertionOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToLineItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLineItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToKpis(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromKpis(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToTrackingPixels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTrackingPixels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAudiences(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAudiences(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToReports(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromReports(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
