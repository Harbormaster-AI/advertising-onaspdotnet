using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IConversionEventRepository
{
    Task<ConversionEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConversionEvent>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ConversionEvent conversionEvent, CancellationToken cancellationToken);
    Task UpdateAsync(ConversionEvent conversionEvent, CancellationToken cancellationToken);
    Task DeleteAsync(ConversionEvent conversionEvent, CancellationToken cancellationToken);


}
