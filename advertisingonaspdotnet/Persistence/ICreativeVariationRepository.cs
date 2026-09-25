using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface ICreativeVariationRepository
{
    Task<CreativeVariation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CreativeVariation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CreativeVariation creativeVariation, CancellationToken cancellationToken);
    Task UpdateAsync(CreativeVariation creativeVariation, CancellationToken cancellationToken);
    Task DeleteAsync(CreativeVariation creativeVariation, CancellationToken cancellationToken);


}
