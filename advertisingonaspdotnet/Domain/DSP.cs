
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class DSP
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DspId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual string? Region { get; set; } 
public virtual ICollection<AdAccount> AdAccounts { get; set; } = new List<AdAccount>();

    public static DSP FromRequest(DSPRequest request) {
        return new DSP {
            Id = request.Id,
            Name = request.Name,
            Website = request.Website,
            Region = request.Region,
        };
    }
}
