using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IDataProviderRepository
{
    Task<DataProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataProvider>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DataProvider dataProvider, CancellationToken cancellationToken);
    Task UpdateAsync(DataProvider dataProvider, CancellationToken cancellationToken);
    Task DeleteAsync(DataProvider dataProvider, CancellationToken cancellationToken);

    Task AddToAudienceSegmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAudienceSegmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
