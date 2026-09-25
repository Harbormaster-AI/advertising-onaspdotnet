
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class ExperimentVariantRepository : IExperimentVariantRepository
{
    private readonly ApplicationDbContext _db;

    public ExperimentVariantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ExperimentVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ExperimentVariants
            .Include(x => x.Experiment)
            .Include(x => x.CreativeVariation)
            .Include(x => x.LineItem)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ExperimentVariant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ExperimentVariants
            .AsNoTracking()
            .Include(x => x.Experiment)
            .Include(x => x.CreativeVariation)
            .Include(x => x.LineItem)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ExperimentVariant experimentVariant, CancellationToken cancellationToken)
    {
        _db.ExperimentVariants.Add(experimentVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ExperimentVariant experimentVariant, CancellationToken cancellationToken)
    {
        _db.ExperimentVariants.Update(experimentVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ExperimentVariant experimentVariant, CancellationToken cancellationToken)
    {
        _db.ExperimentVariants.Remove(experimentVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
