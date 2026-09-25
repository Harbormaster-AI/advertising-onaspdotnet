
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class BillingProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BillingprofileId { get; set; } 
 public virtual string? BillingName { get; set; } 
 public virtual string? TaxId { get; set; } 
 public virtual Address? BillingAddress { get; set; } 
public virtual Advertiser? Advertiser { get; set; } 
public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
public virtual ICollection<AdAccount> AdAccounts { get; set; } = new List<AdAccount>();
 public virtual PaymentTerms? PaymentTerms { get; set; } 

    public static BillingProfile FromRequest(BillingProfileRequest request) {
        return new BillingProfile {
            Id = request.Id,
            BillingName = request.BillingName,
            TaxId = request.TaxId,
            BillingAddress = request.BillingAddress,
            PaymentTerms = request.PaymentTerms,
        };
    }
}
