using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IAdSlotRepository
{
    Task<AdSlot?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AdSlot>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AdSlot adSlot, CancellationToken cancellationToken);
    Task UpdateAsync(AdSlot adSlot, CancellationToken cancellationToken);
    Task DeleteAsync(AdSlot adSlot, CancellationToken cancellationToken);

    Task AddToPlacementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPlacementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
