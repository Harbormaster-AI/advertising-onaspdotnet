
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? UserId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual Email? Email { get; set; } 
public virtual Agency? Agency { get; set; } 
public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
public virtual ICollection<AdAccount> AdAccounts { get; set; } = new List<AdAccount>();
 public virtual AccountRole? Role { get; set; } 

    public static User FromRequest(UserRequest request) {
        return new User {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Role = request.Role,
        };
    }
}
