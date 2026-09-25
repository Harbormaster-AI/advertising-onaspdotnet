
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class ExperimentVariant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ExperimentvariantId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Percentage? Allocation { get; set; } 
public virtual Experiment? Experiment { get; set; } 
public virtual CreativeVariation? CreativeVariation { get; set; } 
public virtual LineItem? LineItem { get; set; } 

    public static ExperimentVariant FromRequest(ExperimentVariantRequest request) {
        return new ExperimentVariant {
            Id = request.Id,
            Name = request.Name,
            Allocation = request.Allocation,
        };
    }
}
