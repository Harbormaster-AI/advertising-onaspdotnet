using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IAdvertiserService {

    Task Create(Advertiser model , CancellationToken cancellationToken);
    Task<bool> Update(Advertiser model, CancellationToken cancellationToken);
    Task<Advertiser?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Advertiser>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAgency(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAgency(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAdAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAdAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToBillingProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBillingProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTrackingPixels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTrackingPixels(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AdvertiserService : IAdvertiserService
{
    private readonly IAdvertiserRepository _repository;
    private readonly ILogger<AdvertiserService> _logger;

    public AdvertiserService(
        IAdvertiserRepository repository, ILogger<AdvertiserService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Advertiser model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Advertiser model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LegalName = model.LegalName;
            existing.Industry = model.Industry;
            existing.Website = model.Website;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Advertiser?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Advertiser>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignAgency(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAgency(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToAdAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAdAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToBillingProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromBillingProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToTrackingPixels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTrackingPixels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
