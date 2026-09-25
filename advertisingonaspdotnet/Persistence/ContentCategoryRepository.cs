
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class ContentCategoryRepository : IContentCategoryRepository
{
    private readonly ApplicationDbContext _db;

    public ContentCategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ContentCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ContentCategorys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ContentCategory>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ContentCategorys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ContentCategory contentCategory, CancellationToken cancellationToken)
    {
        _db.ContentCategorys.Add(contentCategory);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ContentCategory contentCategory, CancellationToken cancellationToken)
    {
        _db.ContentCategorys.Update(contentCategory);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ContentCategory contentCategory, CancellationToken cancellationToken)
    {
        _db.ContentCategorys.Remove(contentCategory);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
