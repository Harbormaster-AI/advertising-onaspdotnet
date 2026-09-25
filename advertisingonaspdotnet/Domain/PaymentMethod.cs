
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class PaymentMethod
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PaymentmethodId { get; set; } 
 public virtual string? Last4 { get; set; } 
 public virtual string? CardholderName { get; set; } 
 public virtual Address? BillingAddress { get; set; } 
public virtual BillingProfile? BillingProfile { get; set; } 
 public virtual PaymentMethodType? MethodType { get; set; } 

    public static PaymentMethod FromRequest(PaymentMethodRequest request) {
        return new PaymentMethod {
            Id = request.Id,
            Last4 = request.Last4,
            CardholderName = request.CardholderName,
            BillingAddress = request.BillingAddress,
            MethodType = request.MethodType,
        };
    }
}
