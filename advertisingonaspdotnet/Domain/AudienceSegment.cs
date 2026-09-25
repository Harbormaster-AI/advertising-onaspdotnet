
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class AudienceSegment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AudiencesegmentId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual int? EstimatedReach { get; set; } 
 public virtual string? Description { get; set; } 
public virtual DataProvider? Provider { get; set; } 
public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
 public virtual DataProviderType? ProviderType { get; set; } 

    public static AudienceSegment FromRequest(AudienceSegmentRequest request) {
        return new AudienceSegment {
            Id = request.Id,
            Name = request.Name,
            EstimatedReach = request.EstimatedReach,
            Description = request.Description,
            ProviderType = request.ProviderType,
        };
    }
}
