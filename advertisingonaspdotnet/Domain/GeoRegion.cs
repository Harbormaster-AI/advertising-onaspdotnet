
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class GeoRegion
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? GeoregionId { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual string? Name { get; set; } 
public virtual GeoRegion? Parent { get; set; } 
public virtual ICollection<GeoRegion> Children { get; set; } = new List<GeoRegion>();
 public virtual GeoRegionType? RegionType { get; set; } 

    public static GeoRegion FromRequest(GeoRegionRequest request) {
        return new GeoRegion {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
            RegionType = request.RegionType,
        };
    }
}
