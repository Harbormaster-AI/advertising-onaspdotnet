
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class TrackingPixel
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TrackingpixelId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual URL? Url { get; set; } 
public virtual Campaign? Campaign { get; set; } 
public virtual Advertiser? Advertiser { get; set; } 
public virtual ICollection<ConversionEvent> ConversionEvents { get; set; } = new List<ConversionEvent>();
 public virtual ConversionEventType? EventType { get; set; } 
 public virtual PixelType? PixelType { get; set; } 

    public static TrackingPixel FromRequest(TrackingPixelRequest request) {
        return new TrackingPixel {
            Id = request.Id,
            Name = request.Name,
            Url = request.Url,
            EventType = request.EventType,
            PixelType = request.PixelType,
        };
    }
}
