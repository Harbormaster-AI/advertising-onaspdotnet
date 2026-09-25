using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Persistence;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Team>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Team team, CancellationToken cancellationToken);
    Task UpdateAsync(Team team, CancellationToken cancellationToken);
    Task DeleteAsync(Team team, CancellationToken cancellationToken);

    Task AddToUsersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromUsersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAdAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAdAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
