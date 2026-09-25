
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class CreativeFileRepository : ICreativeFileRepository
{
    private readonly ApplicationDbContext _db;

    public CreativeFileRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CreativeFile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CreativeFiles
            .Include(x => x.CreativeAsset)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CreativeFile>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CreativeFiles
            .AsNoTracking()
            .Include(x => x.CreativeAsset)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CreativeFile creativeFile, CancellationToken cancellationToken)
    {
        _db.CreativeFiles.Add(creativeFile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CreativeFile creativeFile, CancellationToken cancellationToken)
    {
        _db.CreativeFiles.Update(creativeFile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CreativeFile creativeFile, CancellationToken cancellationToken)
    {
        _db.CreativeFiles.Remove(creativeFile);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
