
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class TargetingProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TargetingprofileId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual ICollection<AudienceSegment> AudienceSegments { get; set; } = new List<AudienceSegment>();
public virtual ICollection<GeoRegion> GeoRegions { get; set; } = new List<GeoRegion>();
public virtual ICollection<ContentCategory> ContentCategories { get; set; } = new List<ContentCategory>();
public virtual BrandSafetyPolicy? BrandSafetyPolicy { get; set; } 
public virtual ICollection<DeviceCriterion> DeviceCriteria { get; set; } = new List<DeviceCriterion>();

    public static TargetingProfile FromRequest(TargetingProfileRequest request) {
        return new TargetingProfile {
            Id = request.Id,
            Name = request.Name,
        };
    }
}
