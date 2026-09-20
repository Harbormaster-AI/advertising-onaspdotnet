using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface IBillingProfileRepository
{
    Task<BillingProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BillingProfile>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BillingProfile billingProfile, CancellationToken cancellationToken);
    Task UpdateAsync(BillingProfile billingProfile, CancellationToken cancellationToken);
    Task DeleteAsync(BillingProfile billingProfile, CancellationToken cancellationToken);
}
