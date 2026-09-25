using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IRateCardRepository
{
    Task<RateCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<RateCard>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(RateCard rateCard, CancellationToken cancellationToken);
    Task UpdateAsync(RateCard rateCard, CancellationToken cancellationToken);
    Task DeleteAsync(RateCard rateCard, CancellationToken cancellationToken);

    Task AddToRatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
