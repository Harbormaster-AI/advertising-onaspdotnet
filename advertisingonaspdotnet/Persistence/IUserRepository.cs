using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task DeleteAsync(User user, CancellationToken cancellationToken);

    Task AddToTeamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTeamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAdAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAdAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
