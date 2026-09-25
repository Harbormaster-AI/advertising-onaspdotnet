
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class RateCard
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RatecardId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual string? Currency { get; set; } 
public virtual Publisher? Publisher { get; set; } 
public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();

    public static RateCard FromRequest(RateCardRequest request) {
        return new RateCard {
            Id = request.Id,
            Name = request.Name,
            EffectiveDate = request.EffectiveDate,
            Currency = request.Currency,
        };
    }
}
