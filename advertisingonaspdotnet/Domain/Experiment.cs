
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class Experiment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ExperimentId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Hypothesis { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
public virtual Campaign? Campaign { get; set; } 
public virtual ICollection<ExperimentVariant> Variants { get; set; } = new List<ExperimentVariant>();
 public virtual ExperimentStatus? Status { get; set; } 

    public static Experiment FromRequest(ExperimentRequest request) {
        return new Experiment {
            Id = request.Id,
            Name = request.Name,
            Hypothesis = request.Hypothesis,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
        };
    }
}
