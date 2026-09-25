
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class Campaign
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CampaignId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Money? TotalBudget { get; set; } 
 public virtual DateRange? Flight { get; set; } 
public virtual AdAccount? AdAccount { get; set; } 
public virtual ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
public virtual ICollection<KPI> Kpis { get; set; } = new List<KPI>();
public virtual ICollection<TrackingPixel> TrackingPixels { get; set; } = new List<TrackingPixel>();
public virtual ICollection<AudienceSegment> Audiences { get; set; } = new List<AudienceSegment>();
public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
public virtual InsertionOrder? InsertionOrder { get; set; } 
 public virtual ObjectiveType? Objective { get; set; } 
 public virtual CampaignStatus? Status { get; set; } 

    public static Campaign FromRequest(CampaignRequest request) {
        return new Campaign {
            Id = request.Id,
            Name = request.Name,
            TotalBudget = request.TotalBudget,
            Flight = request.Flight,
            Objective = request.Objective,
            Status = request.Status,
        };
    }
}
