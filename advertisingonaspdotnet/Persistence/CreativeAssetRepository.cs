
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class CreativeAssetRepository : ICreativeAssetRepository
{
    private readonly ApplicationDbContext _db;

    public CreativeAssetRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CreativeAsset?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CreativeAssets
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CreativeAsset>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CreativeAssets
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken)
    {
        _db.CreativeAssets.Add(creativeAsset);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken)
    {
        _db.CreativeAssets.Update(creativeAsset);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CreativeAsset creativeAsset, CancellationToken cancellationToken)
    {
        _db.CreativeAssets.Remove(creativeAsset);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToFilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CreativeFiles
            .Where(creativeFile =>
                request.ChildIds.Contains(creativeFile.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    creativeFile =>
                        EF.Property<Guid?>(
                            creativeFile,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromFilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CreativeFiles
            .Where(creativeFile =>
                request.ChildIds.Contains(creativeFile.Id) &&
                EF.Property<Guid?>(
                    creativeFile,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    creativeFile =>
                        EF.Property<Guid?>(
                            creativeFile,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToApprovalsAsync(
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

    public async Task RemoveFromApprovalsAsync(
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


    public async Task AddToVariationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CreativeVariations
            .Where(creativeVariation =>
                request.ChildIds.Contains(creativeVariation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    creativeVariation =>
                        EF.Property<Guid?>(
                            creativeVariation,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromVariationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CreativeVariations
            .Where(creativeVariation =>
                request.ChildIds.Contains(creativeVariation.Id) &&
                EF.Property<Guid?>(
                    creativeVariation,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    creativeVariation =>
                        EF.Property<Guid?>(
                            creativeVariation,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToLineItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LineItems
            .Where(lineItem =>
                request.ChildIds.Contains(lineItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    lineItem =>
                        EF.Property<Guid?>(
                            lineItem,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLineItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LineItems
            .Where(lineItem =>
                request.ChildIds.Contains(lineItem.Id) &&
                EF.Property<Guid?>(
                    lineItem,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    lineItem =>
                        EF.Property<Guid?>(
                            lineItem,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
