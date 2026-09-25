
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class BrandSafetyPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BrandsafetypolicyId { get; set; } 
public virtual ICollection<TargetingProfile> TargetingProfiles { get; set; } = new List<TargetingProfile>();
 public virtual BrandSafetyLevel? Level { get; set; } 
 public virtual ContentRating? ContentRatingThreshold { get; set; } 

    public static BrandSafetyPolicy FromRequest(BrandSafetyPolicyRequest request) {
        return new BrandSafetyPolicy {
            Id = request.Id,
            Level = request.Level,
            ContentRatingThreshold = request.ContentRatingThreshold,
        };
    }
}
