using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IBillingProfileRepository
{
    Task<BillingProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BillingProfile>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BillingProfile billingProfile, CancellationToken cancellationToken);
    Task UpdateAsync(BillingProfile billingProfile, CancellationToken cancellationToken);
    Task DeleteAsync(BillingProfile billingProfile, CancellationToken cancellationToken);

    Task AddToPaymentMethodsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentMethodsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAdAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAdAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
