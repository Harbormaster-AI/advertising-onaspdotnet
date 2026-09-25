
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class DeviceCriterion
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DevicecriterionId { get; set; } 
public virtual TargetingProfile? TargetingProfile { get; set; } 
 public virtual DeviceType? DeviceType { get; set; } 
 public virtual PlatformType? PlatformType { get; set; } 
 public virtual TargetingOperator? Operator_ { get; set; } 

    public static DeviceCriterion FromRequest(DeviceCriterionRequest request) {
        return new DeviceCriterion {
            Id = request.Id,
            DeviceType = request.DeviceType,
            PlatformType = request.PlatformType,
            Operator_ = request.Operator_,
        };
    }
}
