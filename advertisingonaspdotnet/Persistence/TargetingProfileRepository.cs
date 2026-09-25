
using advertisingonaspdotnet.Contracts;
using advertisingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace advertisingonaspdotnet.Persistence;

public class TargetingProfileRepository : ITargetingProfileRepository
{
    private readonly ApplicationDbContext _db;

    public TargetingProfileRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TargetingProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TargetingProfiles
            .Include(x => x.BrandSafetyPolicy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TargetingProfile>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TargetingProfiles
            .AsNoTracking()
            .Include(x => x.BrandSafetyPolicy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken)
    {
        _db.TargetingProfiles.Add(targetingProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken)
    {
        _db.TargetingProfiles.Update(targetingProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TargetingProfile targetingProfile, CancellationToken cancellationToken)
    {
        _db.TargetingProfiles.Remove(targetingProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAudienceSegmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AudienceSegments
            .Where(audienceSegment =>
                request.ChildIds.Contains(audienceSegment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    audienceSegment =>
                        EF.Property<Guid?>(
                            audienceSegment,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAudienceSegmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AudienceSegments
            .Where(audienceSegment =>
                request.ChildIds.Contains(audienceSegment.Id) &&
                EF.Property<Guid?>(
                    audienceSegment,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    audienceSegment =>
                        EF.Property<Guid?>(
                            audienceSegment,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToGeoRegionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GeoRegions
            .Where(geoRegion =>
                request.ChildIds.Contains(geoRegion.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    geoRegion =>
                        EF.Property<Guid?>(
                            geoRegion,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromGeoRegionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GeoRegions
            .Where(geoRegion =>
                request.ChildIds.Contains(geoRegion.Id) &&
                EF.Property<Guid?>(
                    geoRegion,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    geoRegion =>
                        EF.Property<Guid?>(
                            geoRegion,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToContentCategoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ContentCategorys
            .Where(contentCategory =>
                request.ChildIds.Contains(contentCategory.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    contentCategory =>
                        EF.Property<Guid?>(
                            contentCategory,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromContentCategoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ContentCategorys
            .Where(contentCategory =>
                request.ChildIds.Contains(contentCategory.Id) &&
                EF.Property<Guid?>(
                    contentCategory,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    contentCategory =>
                        EF.Property<Guid?>(
                            contentCategory,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }


    public async Task AddToDeviceCriteriaAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceCriterions
            .Where(deviceCriterion =>
                request.ChildIds.Contains(deviceCriterion.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceCriterion =>
                        EF.Property<Guid?>(
                            deviceCriterion,
                            "GeoRegion_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDeviceCriteriaAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceCriterions
            .Where(deviceCriterion =>
                request.ChildIds.Contains(deviceCriterion.Id) &&
                EF.Property<Guid?>(
                    deviceCriterion,
                    "GeoRegion_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceCriterion =>
                        EF.Property<Guid?>(
                            deviceCriterion,
                            "GeoRegion_Id"),
                    (Guid?)null));
    }

}
