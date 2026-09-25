
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class InventorySource
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InventorysourceId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Domain { get; set; } 
public virtual Publisher? Publisher { get; set; } 
public virtual ICollection<AdSlot> AdSlots { get; set; } = new List<AdSlot>();
public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();
 public virtual ChannelType? Channel { get; set; } 
 public virtual AdFormat? PrimaryFormat { get; set; } 

    public static InventorySource FromRequest(InventorySourceRequest request) {
        return new InventorySource {
            Id = request.Id,
            Name = request.Name,
            Domain = request.Domain,
            Channel = request.Channel,
            PrimaryFormat = request.PrimaryFormat,
        };
    }
}
