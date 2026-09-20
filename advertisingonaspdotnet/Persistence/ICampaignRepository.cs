using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public interface ICampaignRepository
{
    Task<Campaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Campaign>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Campaign campaign, CancellationToken cancellationToken);
    Task UpdateAsync(Campaign campaign, CancellationToken cancellationToken);
    Task DeleteAsync(Campaign campaign, CancellationToken cancellationToken);
}
