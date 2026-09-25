using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface ICreativeAssetRepository
{
    Task<CreativeAsset?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CreativeAsset>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken);
    Task UpdateAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken);
    Task DeleteAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken);

    Task AddToFilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToApprovalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromApprovalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToVariationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromVariationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLineItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLineItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
