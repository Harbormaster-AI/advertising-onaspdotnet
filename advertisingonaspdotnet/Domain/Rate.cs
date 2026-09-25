
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class Rate
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RateId { get; set; } 
 public virtual Money? UnitPrice { get; set; } 
public virtual RateCard? RateCard { get; set; } 
public virtual AdSlot? AdSlot { get; set; } 
 public virtual AdFormat? AdFormat { get; set; } 
 public virtual PricingModel? PricingModel { get; set; } 

    public static Rate FromRequest(RateRequest request) {
        return new Rate {
            Id = request.Id,
            UnitPrice = request.UnitPrice,
            AdFormat = request.AdFormat,
            PricingModel = request.PricingModel,
        };
    }
}
