using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IAdAccountService {

    Task Create(AdAccount model , CancellationToken cancellationToken);
    Task<bool> Update(AdAccount model, CancellationToken cancellationToken);
    Task<AdAccount?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AdAccount>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAdvertiser(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAdvertiser(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignBillingProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBillingProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDsp(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDsp(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToUsers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromUsers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPerformanceMetrics(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPerformanceMetrics(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AdAccountService : IAdAccountService
{
    private readonly IAdAccountRepository _repository;
    private readonly ILogger<AdAccountService> _logger;

    public AdAccountService(
        IAdAccountRepository repository, ILogger<AdAccountService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AdAccount model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(AdAccount model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.AccountCode = model.AccountCode;
            existing.DefaultCurrency = model.DefaultCurrency;
            existing.DefaultTimezone = model.DefaultTimezone;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<AdAccount?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AdAccount>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignAdvertiser(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAdvertiser(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignBillingProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignBillingProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignDsp(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDsp(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToUsers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromUsers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPerformanceMetrics(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPerformanceMetrics(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
