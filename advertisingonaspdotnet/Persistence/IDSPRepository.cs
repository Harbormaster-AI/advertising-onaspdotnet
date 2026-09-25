using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IDSPRepository
{
    Task<DSP?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DSP>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DSP dSP, CancellationToken cancellationToken);
    Task UpdateAsync(DSP dSP, CancellationToken cancellationToken);
    Task DeleteAsync(DSP dSP, CancellationToken cancellationToken);

    Task AddToAdAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAdAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
