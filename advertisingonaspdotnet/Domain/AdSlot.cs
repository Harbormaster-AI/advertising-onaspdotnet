
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class AdSlot
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AdslotId { get; set; } 
 public virtual string? SlotCode { get; set; } 
 public virtual int? Width { get; set; } 
 public virtual int? Height { get; set; } 
 public virtual Money? FloorPrice { get; set; } 
public virtual InventorySource? InventorySource { get; set; } 
public virtual ICollection<Placement> Placements { get; set; } = new List<Placement>();
public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
 public virtual AdFormat? Format { get; set; } 

    public static AdSlot FromRequest(AdSlotRequest request) {
        return new AdSlot {
            Id = request.Id,
            SlotCode = request.SlotCode,
            Width = request.Width,
            Height = request.Height,
            FloorPrice = request.FloorPrice,
            Format = request.Format,
        };
    }
}
