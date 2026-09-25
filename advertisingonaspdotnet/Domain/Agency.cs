
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class Agency
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AgencyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? HeadquartersCountry { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<Advertiser> Advertisers { get; set; } = new List<Advertiser>();
public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
public virtual ICollection<User> Users { get; set; } = new List<User>();
public virtual ICollection<InsertionOrder> InsertionOrders { get; set; } = new List<InsertionOrder>();

    public static Agency FromRequest(AgencyRequest request) {
        return new Agency {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,
        };
    }
}
