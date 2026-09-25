
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Telemetry;

namespace advertisingonaspdotnet.Service;

public interface IPaymentMethodService {

    Task Create(PaymentMethod model , CancellationToken cancellationToken);
    Task<bool> Update(PaymentMethod model, CancellationToken cancellationToken);
    Task<PaymentMethod?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentMethod>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignBillingProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBillingProfile(AssociationRequest request, CancellationToken cancellationToken);


}

public class PaymentMethodService : IPaymentMethodService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPaymentMethodRepository _repository;
    private readonly ILogger<PaymentMethodService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PaymentMethodService(
        ApplicationTelemetry telemetry,
        IPaymentMethodRepository repository,
        ILogger<PaymentMethodService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(PaymentMethod model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "PaymentMethod",
                "CreatePaymentMethod",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(PaymentMethod model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Last4 = model.Last4;
            existing.CardholderName = model.CardholderName;
            existing.BillingAddress = model.BillingAddress;
            existing.MethodType = model.MethodType;

            await _telemetry.Execute(
                "PaymentMethod",
                "UpdatePaymentMethod",
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

    public Task<PaymentMethod?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PaymentMethod>> GetAll(CancellationToken cancellationToken)
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
                "PaymentMethod",
                "UpdatePaymentMethod",
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

    public async Task<bool> AssignBillingProfile(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No PaymentMethod found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<BillingProfileService>().Get(childRequest, cancellationToken);
            parent.BillingProfile = child;
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

    public async Task<bool> UnassignBillingProfile(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No PaymentMethod found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.BillingProfile = null;
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




}
