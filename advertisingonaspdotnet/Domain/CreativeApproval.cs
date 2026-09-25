
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class CreativeApproval
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CreativeapprovalId { get; set; } 
 public virtual string? Reviewer { get; set; } 
 public virtual DateOnly? ReviewedAt { get; set; } 
public virtual CreativeAsset? CreativeAsset { get; set; } 
public virtual Publisher? Publisher { get; set; } 
 public virtual CreativeApprovalStatus? Status { get; set; } 

    public static CreativeApproval FromRequest(CreativeApprovalRequest request) {
        return new CreativeApproval {
            Id = request.Id,
            Reviewer = request.Reviewer,
            ReviewedAt = request.ReviewedAt,
            Status = request.Status,
        };
    }
}
