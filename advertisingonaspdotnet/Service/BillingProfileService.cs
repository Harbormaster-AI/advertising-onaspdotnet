
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IBillingProfileRepository _repository;
    private readonly ILogger<BillingProfileService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public BillingProfileService(
        ApplicationTelemetry telemetry,
        IBillingProfileRepository repository,
        ILogger<BillingProfileService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(BillingProfile model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "BillingProfile",
                "CreateBillingProfile",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "BillingProfile",
                "UpdateBillingProfile",
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
            await _telemetry.Execute(
                "BillingProfile",
                "UpdateBillingProfile",
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

    public async Task<bool> AssignAdvertiser(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No BillingProfile found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AdvertiserService>().Get(childRequest, cancellationToken);
            parent.Advertiser = child;
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

    public async Task<bool> UnassignAdvertiser(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No BillingProfile found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Advertiser = null;
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


    public async Task<bool> AddToPaymentMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BillingProfile",
                "AddToPaymentMethods",
                () => _repository.AddToPaymentMethodsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPaymentMethods(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BillingProfile",
                "RemoveFromPaymentMethods",
                () => _repository.RemoveFromPaymentMethodsAsync(request, cancellationToken));
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

    public async Task<bool> AddToAdAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BillingProfile",
                "AddToAdAccounts",
                () => _repository.AddToAdAccountsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAdAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BillingProfile",
                "RemoveFromAdAccounts",
                () => _repository.RemoveFromAdAccountsAsync(request, cancellationToken));
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
