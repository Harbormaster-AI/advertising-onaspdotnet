
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class LineItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LineitemId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Money? BidAmount { get; set; } 
 public virtual Money? DailyBudget { get; set; } 
 public virtual FrequencyCap? FrequencyCap { get; set; } 
public virtual Campaign? Campaign { get; set; } 
public virtual ICollection<Placement> Placements { get; set; } = new List<Placement>();
public virtual TargetingProfile? TargetingProfile { get; set; } 
public virtual Deal? Deal { get; set; } 
public virtual ICollection<CreativeAsset> Creatives { get; set; } = new List<CreativeAsset>();
public virtual ICollection<PerformanceMetric> PerformanceMetrics { get; set; } = new List<PerformanceMetric>();
 public virtual LineItemStatus? Status { get; set; } 
 public virtual PricingModel? PricingModel { get; set; } 
 public virtual BidStrategyType? BidStrategy { get; set; } 
 public virtual PacingType? Pacing { get; set; } 

    public static LineItem FromRequest(LineItemRequest request) {
        return new LineItem {
            Id = request.Id,
            Name = request.Name,
            BidAmount = request.BidAmount,
            DailyBudget = request.DailyBudget,
            FrequencyCap = request.FrequencyCap,
            Status = request.Status,
            PricingModel = request.PricingModel,
            BidStrategy = request.BidStrategy,
            Pacing = request.Pacing,
        };
    }
}
