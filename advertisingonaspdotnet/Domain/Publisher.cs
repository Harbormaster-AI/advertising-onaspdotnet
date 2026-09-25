
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class Publisher
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PublisherId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<InventorySource> InventorySources { get; set; } = new List<InventorySource>();
public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();
public virtual ICollection<CreativeApproval> CreativeApprovals { get; set; } = new List<CreativeApproval>();
public virtual ICollection<InsertionOrder> InsertionOrders { get; set; } = new List<InsertionOrder>();
public virtual ICollection<RateCard> RateCards { get; set; } = new List<RateCard>();
 public virtual PublisherType? PublisherType { get; set; } 

    public static Publisher FromRequest(PublisherRequest request) {
        return new Publisher {
            Id = request.Id,
            Name = request.Name,
            Website = request.Website,
            PublisherType = request.PublisherType,
        };
    }
}
