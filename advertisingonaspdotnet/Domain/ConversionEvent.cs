
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class ConversionEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ConversioneventId { get; set; } 
 public virtual DateTime? Timestamp { get; set; } 
 public virtual Money? Value { get; set; } 
public virtual Campaign? Campaign { get; set; } 
public virtual LineItem? LineItem { get; set; } 
public virtual TrackingPixel? TrackingPixel { get; set; } 
 public virtual ConversionEventType? EventType { get; set; } 
 public virtual AttributionModel? AttributionModel { get; set; } 

    public static ConversionEvent FromRequest(ConversionEventRequest request) {
        return new ConversionEvent {
            Id = request.Id,
            Timestamp = request.Timestamp,
            Value = request.Value,
            EventType = request.EventType,
            AttributionModel = request.AttributionModel,
        };
    }
}
