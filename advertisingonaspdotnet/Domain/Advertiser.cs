
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class Advertiser
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AdvertiserId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? Industry { get; set; } 
 public virtual string? Website { get; set; } 
public virtual Agency? Agency { get; set; } 
public virtual ICollection<AdAccount> AdAccounts { get; set; } = new List<AdAccount>();
public virtual ICollection<BillingProfile> BillingProfiles { get; set; } = new List<BillingProfile>();
public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
public virtual ICollection<TrackingPixel> TrackingPixels { get; set; } = new List<TrackingPixel>();

    public static Advertiser FromRequest(AdvertiserRequest request) {
        return new Advertiser {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            Industry = request.Industry,
            Website = request.Website,
        };
    }
}
