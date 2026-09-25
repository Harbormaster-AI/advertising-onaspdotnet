
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class ConversionEventRepository : IConversionEventRepository
{
    private readonly ApplicationDbContext _db;

    public ConversionEventRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ConversionEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ConversionEvents
            .Include(x => x.Campaign)
            .Include(x => x.LineItem)
            .Include(x => x.TrackingPixel)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ConversionEvent>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ConversionEvents
            .AsNoTracking()
            .Include(x => x.Campaign)
            .Include(x => x.LineItem)
            .Include(x => x.TrackingPixel)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ConversionEvent conversionEvent, CancellationToken cancellationToken)
    {
        _db.ConversionEvents.Add(conversionEvent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ConversionEvent conversionEvent, CancellationToken cancellationToken)
    {
        _db.ConversionEvents.Update(conversionEvent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ConversionEvent conversionEvent, CancellationToken cancellationToken)
    {
        _db.ConversionEvents.Remove(conversionEvent);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
