using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IBillingProfileService {

    Task Create(BillingProfile model , CancellationToken cancellationToken);
    Task<bool> Update(BillingProfile model, CancellationToken cancellationToken);
    Task<BillingProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BillingProfile>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAdvertiser(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAdvertiser(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPaymentMethods(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPaymentMethods(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAdAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAdAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class BillingProfileService : IBillingProfileService
{
    private readonly IBillingProfileRepository _repository;
    private readonly ILogger<BillingProfileService> _logger;

    public BillingProfileService(
        IBillingProfileRepository repository, ILogger<BillingProfileService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(BillingProfile model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(BillingProfile model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.BillingName = model.BillingName;
            existing.TaxId = model.TaxId;
            existing.BillingAddress = model.BillingAddress;
            existing.PaymentTerms = model.PaymentTerms;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<BillingProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BillingProfile>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToPaymentMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPaymentMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAdAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAdAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
