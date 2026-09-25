
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class InsertionOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InsertionorderId { get; set; } 
 public virtual string? IoNumber { get; set; } 
 public virtual Money? AgreedBudget { get; set; } 
 public virtual DateRange? Flight { get; set; } 
public virtual Advertiser? Advertiser { get; set; } 
public virtual Agency? Agency { get; set; } 
public virtual Publisher? Publisher { get; set; } 
public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
 public virtual IOStatus? Status { get; set; } 

    public static InsertionOrder FromRequest(InsertionOrderRequest request) {
        return new InsertionOrder {
            Id = request.Id,
            IoNumber = request.IoNumber,
            AgreedBudget = request.AgreedBudget,
            Flight = request.Flight,
            Status = request.Status,
        };
    }
}
