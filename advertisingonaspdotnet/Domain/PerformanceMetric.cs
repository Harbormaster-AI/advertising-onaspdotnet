
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class PerformanceMetric
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PerformancemetricId { get; set; } 
 public virtual DateOnly? Date { get; set; } 
 public virtual decimal? Value { get; set; } 
public virtual AdAccount? AdAccount { get; set; } 
public virtual Campaign? Campaign { get; set; } 
public virtual LineItem? LineItem { get; set; } 
public virtual Placement? Placement { get; set; } 
public virtual CreativeAsset? CreativeAsset { get; set; } 
 public virtual MetricType? MetricType { get; set; } 

    public static PerformanceMetric FromRequest(PerformanceMetricRequest request) {
        return new PerformanceMetric {
            Id = request.Id,
            Date = request.Date,
            Value = request.Value,
            MetricType = request.MetricType,
        };
    }
}
