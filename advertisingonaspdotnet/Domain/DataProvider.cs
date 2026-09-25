
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class DataProvider
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DataproviderId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<AudienceSegment> AudienceSegments { get; set; } = new List<AudienceSegment>();
 public virtual DataProviderType? ProviderType { get; set; } 

    public static DataProvider FromRequest(DataProviderRequest request) {
        return new DataProvider {
            Id = request.Id,
            Name = request.Name,
            Website = request.Website,
            ProviderType = request.ProviderType,
        };
    }
}
