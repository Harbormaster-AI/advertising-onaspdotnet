
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class CreativeAsset
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CreativeassetId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual URL? ClickUrl { get; set; } 
 public virtual URL? LandingPage { get; set; } 
 public virtual int? Width { get; set; } 
 public virtual int? Height { get; set; } 
 public virtual int? DurationSeconds { get; set; } 
public virtual ICollection<CreativeFile> Files { get; set; } = new List<CreativeFile>();
public virtual ICollection<CreativeApproval> Approvals { get; set; } = new List<CreativeApproval>();
public virtual ICollection<CreativeVariation> Variations { get; set; } = new List<CreativeVariation>();
public virtual ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
 public virtual CreativeType? CreativeType { get; set; } 
 public virtual AdFormat? AdFormat { get; set; } 

    public static CreativeAsset FromRequest(CreativeAssetRequest request) {
        return new CreativeAsset {
            Id = request.Id,
            Name = request.Name,
            ClickUrl = request.ClickUrl,
            LandingPage = request.LandingPage,
            Width = request.Width,
            Height = request.Height,
            DurationSeconds = request.DurationSeconds,
            CreativeType = request.CreativeType,
            AdFormat = request.AdFormat,
        };
    }
}
