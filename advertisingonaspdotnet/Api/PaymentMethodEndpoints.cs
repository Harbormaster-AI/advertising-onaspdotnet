
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class PaymentMethodEndpoints
{
    public static IEndpointRouteBuilder MapPaymentMethodEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/paymentMethod").WithTags("PaymentMethods");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignBillingProfile", AssignBillingProfile);
        group.MapPut("/unassignBillingProfile", UnassignBillingProfile);


        return app;
    }

    private static async Task<IResult> Create(
        PaymentMethodRequest request,
        IPaymentMethodService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentMethod( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        PaymentMethodRequest request,
        IPaymentMethodService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentMethod( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IPaymentMethodService service,
        CancellationToken cancellationToken) {

        var paymentMethod = await service.Get(identifier, cancellationToken);
        return paymentMethod is null ? Results.NotFound() : Results.Ok( paymentMethod );
    }


    private static async Task<IResult> GetAll(
        IPaymentMethodService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PaymentMethodResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPaymentMethodService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBillingProfile(
        AssociationRequest request,
        IPaymentMethodService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBillingProfile(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBillingProfile(
    AssociationRequest request,
    IPaymentMethodService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBillingProfile(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static PaymentMethod mapRequestToPaymentMethod( PaymentMethodRequest request ) {
        var model = new PaymentMethod
        {
            Id = request.Id,
            Last4 = request.Last4,
            CardholderName = request.CardholderName,
            BillingAddress = request.BillingAddress,
            MethodType = request.MethodType,
        };
        return model;
    }

}
