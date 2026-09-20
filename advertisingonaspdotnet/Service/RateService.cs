using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IRateService {

    Task Create(Rate model , CancellationToken cancellationToken);
    Task<bool> Update(Rate model, CancellationToken cancellationToken);
    Task<Rate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Rate>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRateCard(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRateCard(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAdSlot(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAdSlot(AssociationRequest request, CancellationToken cancellationToken);


}

public class RateService : IRateService
{
    private readonly IRateRepository _repository;
    private readonly ILogger<RateService> _logger;

    public RateService(
        IRateRepository repository, ILogger<RateService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Rate model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Rate model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.UnitPrice = model.UnitPrice;
            existing.AdFormat = model.AdFormat;
            existing.PricingModel = model.PricingModel;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Rate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Rate>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRateCard(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRateCard(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignAdSlot(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAdSlot(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
