
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class CreativeVariation
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CreativevariationId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Language { get; set; } 
 public virtual string? Headline { get; set; } 
 public virtual string? BodyText { get; set; } 
 public virtual string? CallToAction { get; set; } 
public virtual CreativeAsset? CreativeAsset { get; set; } 

    public static CreativeVariation FromRequest(CreativeVariationRequest request) {
        return new CreativeVariation {
            Id = request.Id,
            Name = request.Name,
            Language = request.Language,
            Headline = request.Headline,
            BodyText = request.BodyText,
            CallToAction = request.CallToAction,
        };
    }
}
