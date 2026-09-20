using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface ICreativeApprovalService {

    Task Create(CreativeApproval model , CancellationToken cancellationToken);
    Task<bool> Update(CreativeApproval model, CancellationToken cancellationToken);
    Task<CreativeApproval?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CreativeApproval>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPublisher(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPublisher(AssociationRequest request, CancellationToken cancellationToken);


}

public class CreativeApprovalService : ICreativeApprovalService
{
    private readonly ICreativeApprovalRepository _repository;
    private readonly ILogger<CreativeApprovalService> _logger;

    public CreativeApprovalService(
        ICreativeApprovalRepository repository, ILogger<CreativeApprovalService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CreativeApproval model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(CreativeApproval model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Reviewer = model.Reviewer;
            existing.ReviewedAt = model.ReviewedAt;
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

    public Task<CreativeApproval?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CreativeApproval>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCreativeAsset(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPublisher(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPublisher(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
