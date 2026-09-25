
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class Placement
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PlacementId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual DateRange? Flight { get; set; } 
 public virtual int? GoalImpressions { get; set; } 
public virtual LineItem? LineItem { get; set; } 
public virtual AdSlot? AdSlot { get; set; } 
public virtual Deal? Deal { get; set; } 

    public static Placement FromRequest(PlacementRequest request) {
        return new Placement {
            Id = request.Id,
            Name = request.Name,
            Flight = request.Flight,
            GoalImpressions = request.GoalImpressions,
        };
    }
}
