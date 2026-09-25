
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class Deal
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DealId { get; set; } 
 public virtual Money? FloorPrice { get; set; } 
public virtual Publisher? Publisher { get; set; } 
public virtual ICollection<InventorySource> InventorySources { get; set; } = new List<InventorySource>();
public virtual ICollection<Placement> Placements { get; set; } = new List<Placement>();
 public virtual DealType? DealType { get; set; } 

    public static Deal FromRequest(DealRequest request) {
        return new Deal {
            Id = request.Id,
            FloorPrice = request.FloorPrice,
            DealType = request.DealType,
        };
    }
}
