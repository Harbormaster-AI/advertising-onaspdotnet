using advertisingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class PublisherRepository : IPublisherRepository
{
    private readonly ApplicationDbContext _db;

    public PublisherRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Publisher?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Publishers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Publisher>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Publishers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Publisher publisher, CancellationToken cancellationToken)
    {
        _db.Publishers.Add(publisher);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Publisher publisher, CancellationToken cancellationToken)
    {
        _db.Publishers.Update(publisher);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Publisher publisher, CancellationToken cancellationToken)
    {
        _db.Publishers.Remove(publisher);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
