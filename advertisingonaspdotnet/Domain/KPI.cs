
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class KPI
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? KpiId { get; set; } 
 public virtual decimal? TargetValue { get; set; } 
public virtual Campaign? Campaign { get; set; } 
 public virtual MetricType? MetricType { get; set; } 

    public static KPI FromRequest(KPIRequest request) {
        return new KPI {
            Id = request.Id,
            TargetValue = request.TargetValue,
            MetricType = request.MetricType,
        };
    }
}
