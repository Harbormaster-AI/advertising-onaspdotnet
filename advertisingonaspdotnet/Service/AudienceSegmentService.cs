using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IAudienceSegmentService {

    Task Create(AudienceSegment model , CancellationToken cancellationToken);
    Task<bool> Update(AudienceSegment model, CancellationToken cancellationToken);
    Task<AudienceSegment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AudienceSegment>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignProvider(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProvider(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AudienceSegmentService : IAudienceSegmentService
{
    private readonly IAudienceSegmentRepository _repository;
    private readonly ILogger<AudienceSegmentService> _logger;

    public AudienceSegmentService(
        IAudienceSegmentRepository repository, ILogger<AudienceSegmentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AudienceSegment model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(AudienceSegment model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.EstimatedReach = model.EstimatedReach;
            existing.Description = model.Description;
            existing.ProviderType = model.ProviderType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<AudienceSegment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AudienceSegment>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignProvider(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignProvider(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
