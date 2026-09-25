
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class ContentCategory
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ContentcategoryId { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual string? Name { get; set; } 

    public static ContentCategory FromRequest(ContentCategoryRequest request) {
        return new ContentCategory {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
        };
    }
}
