using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IRateCardService {

    Task Create(RateCard model , CancellationToken cancellationToken);
    Task<bool> Update(RateCard model, CancellationToken cancellationToken);
    Task<RateCard?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<RateCard>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPublisher(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPublisher(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToRates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRates(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class RateCardService : IRateCardService
{
    private readonly IRateCardRepository _repository;
    private readonly ILogger<RateCardService> _logger;

    public RateCardService(
        IRateCardRepository repository, ILogger<RateCardService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(RateCard model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(RateCard model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.EffectiveDate = model.EffectiveDate;
            existing.Currency = model.Currency;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<RateCard?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<RateCard>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPublisher(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPublisher(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToRates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromRates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
