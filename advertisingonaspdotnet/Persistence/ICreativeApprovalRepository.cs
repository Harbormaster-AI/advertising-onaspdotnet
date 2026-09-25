using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface ICreativeApprovalRepository
{
    Task<CreativeApproval?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CreativeApproval>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CreativeApproval creativeApproval, CancellationToken cancellationToken);
    Task UpdateAsync(CreativeApproval creativeApproval, CancellationToken cancellationToken);
    Task DeleteAsync(CreativeApproval creativeApproval, CancellationToken cancellationToken);


}
