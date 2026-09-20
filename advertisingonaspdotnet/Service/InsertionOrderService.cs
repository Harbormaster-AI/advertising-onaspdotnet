using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IInsertionOrderService {

    Task Create(InsertionOrder model , CancellationToken cancellationToken);
    Task<bool> Update(InsertionOrder model, CancellationToken cancellationToken);
    Task<InsertionOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsertionOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAdvertiser(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAdvertiser(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAgency(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAgency(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPublisher(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPublisher(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InsertionOrderService : IInsertionOrderService
{
    private readonly IInsertionOrderRepository _repository;
    private readonly ILogger<InsertionOrderService> _logger;

    public InsertionOrderService(
        IInsertionOrderRepository repository, ILogger<InsertionOrderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(InsertionOrder model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(InsertionOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.IoNumber = model.IoNumber;
            existing.AgreedBudget = model.AgreedBudget;
            existing.Flight = model.Flight;
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

    public Task<InsertionOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InsertionOrder>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignAgency(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAgency(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPublisher(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPublisher(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
