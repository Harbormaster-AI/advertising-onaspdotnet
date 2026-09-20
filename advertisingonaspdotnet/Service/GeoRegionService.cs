using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Service;

public interface IGeoRegionService {

    Task Create(GeoRegion model , CancellationToken cancellationToken);
    Task<bool> Update(GeoRegion model, CancellationToken cancellationToken);
    Task<GeoRegion?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<GeoRegion>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignParent(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignParent(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToChildren(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromChildren(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class GeoRegionService : IGeoRegionService
{
    private readonly IGeoRegionRepository _repository;
    private readonly ILogger<GeoRegionService> _logger;

    public GeoRegionService(
        IGeoRegionRepository repository, ILogger<GeoRegionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(GeoRegion model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(GeoRegion model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Code = model.Code;
            existing.Name = model.Name;
            existing.RegionType = model.RegionType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<GeoRegion?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<GeoRegion>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignParent(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignParent(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToChildren(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromChildren(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
