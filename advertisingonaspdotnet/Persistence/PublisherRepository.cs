
using advertisingonaspdotnet.Contracts;
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


    public async Task AddToInventorySourcesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InventorySources
            .Where(inventorySource =>
                request.ChildIds.Contains(inventorySource.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    inventorySource =>
                        EF.Property<Guid?>(
                            inventorySource,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInventorySourcesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InventorySources
            .Where(inventorySource =>
                request.ChildIds.Contains(inventorySource.Id) &&
                EF.Property<Guid?>(
                    inventorySource,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    inventorySource =>
                        EF.Property<Guid?>(
                            inventorySource,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToDealsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Deals
            .Where(deal =>
                request.ChildIds.Contains(deal.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deal =>
                        EF.Property<Guid?>(
                            deal,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDealsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Deals
            .Where(deal =>
                request.ChildIds.Contains(deal.Id) &&
                EF.Property<Guid?>(
                    deal,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deal =>
                        EF.Property<Guid?>(
                            deal,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToCreativeApprovalsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CreativeApprovals
            .Where(creativeApproval =>
                request.ChildIds.Contains(creativeApproval.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    creativeApproval =>
                        EF.Property<Guid?>(
                            creativeApproval,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCreativeApprovalsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CreativeApprovals
            .Where(creativeApproval =>
                request.ChildIds.Contains(creativeApproval.Id) &&
                EF.Property<Guid?>(
                    creativeApproval,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    creativeApproval =>
                        EF.Property<Guid?>(
                            creativeApproval,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToInsertionOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InsertionOrders
            .Where(insertionOrder =>
                request.ChildIds.Contains(insertionOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    insertionOrder =>
                        EF.Property<Guid?>(
                            insertionOrder,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInsertionOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InsertionOrders
            .Where(insertionOrder =>
                request.ChildIds.Contains(insertionOrder.Id) &&
                EF.Property<Guid?>(
                    insertionOrder,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    insertionOrder =>
                        EF.Property<Guid?>(
                            insertionOrder,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToRateCardsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RateCards
            .Where(rateCard =>
                request.ChildIds.Contains(rateCard.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    rateCard =>
                        EF.Property<Guid?>(
                            rateCard,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRateCardsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RateCards
            .Where(rateCard =>
                request.ChildIds.Contains(rateCard.Id) &&
                EF.Property<Guid?>(
                    rateCard,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    rateCard =>
                        EF.Property<Guid?>(
                            rateCard,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
