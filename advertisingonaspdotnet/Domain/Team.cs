
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class Team
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TeamId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual Agency? Agency { get; set; } 
public virtual ICollection<User> Users { get; set; } = new List<User>();
public virtual ICollection<AdAccount> AdAccounts { get; set; } = new List<AdAccount>();

    public static Team FromRequest(TeamRequest request) {
        return new Team {
            Id = request.Id,
            Name = request.Name,
        };
    }
}
