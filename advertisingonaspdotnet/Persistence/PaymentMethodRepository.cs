
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly ApplicationDbContext _db;

    public PaymentMethodRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PaymentMethod?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PaymentMethods
            .Include(x => x.BillingProfile)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PaymentMethods
            .AsNoTracking()
            .Include(x => x.BillingProfile)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        _db.PaymentMethods.Add(paymentMethod);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        _db.PaymentMethods.Update(paymentMethod);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        _db.PaymentMethods.Remove(paymentMethod);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
