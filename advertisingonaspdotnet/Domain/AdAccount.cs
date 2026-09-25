
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class AdAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AdaccountId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? AccountCode { get; set; } 
 public virtual string? DefaultCurrency { get; set; } 
 public virtual string? DefaultTimezone { get; set; } 
public virtual Advertiser? Advertiser { get; set; } 
public virtual ICollection<User> Users { get; set; } = new List<User>();
public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
public virtual BillingProfile? BillingProfile { get; set; } 
public virtual DSP? Dsp { get; set; } 
public virtual ICollection<PerformanceMetric> PerformanceMetrics { get; set; } = new List<PerformanceMetric>();

    public static AdAccount FromRequest(AdAccountRequest request) {
        return new AdAccount {
            Id = request.Id,
            Name = request.Name,
            AccountCode = request.AccountCode,
            DefaultCurrency = request.DefaultCurrency,
            DefaultTimezone = request.DefaultTimezone,
        };
    }
}
