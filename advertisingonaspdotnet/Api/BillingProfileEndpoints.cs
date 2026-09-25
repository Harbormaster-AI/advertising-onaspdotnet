
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class BillingProfileEndpoints
{
    public static IEndpointRouteBuilder MapBillingProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/billingProfile").WithTags("BillingProfiles");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAdvertiser", AssignAdvertiser);
        group.MapPut("/unassignAdvertiser", UnassignAdvertiser);

    group.MapPut("/addToPaymentMethods", AddToPaymentMethods);
    group.MapPut("/removeFromPaymentMethods", RemoveFromPaymentMethods);

    group.MapPut("/addToAdAccounts", AddToAdAccounts);
    group.MapPut("/removeFromAdAccounts", RemoveFromAdAccounts);


        return app;
    }

    private static async Task<IResult> Create(
        BillingProfileRequest request,
        IBillingProfileService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBillingProfile( request );

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
        BillingProfileRequest request,
        IBillingProfileService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBillingProfile( request );

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
        IBillingProfileService service,
        CancellationToken cancellationToken) {

        var billingProfile = await service.Get(identifier, cancellationToken);
        return billingProfile is null ? Results.NotFound() : Results.Ok( billingProfile );
    }


    private static async Task<IResult> GetAll(
        IBillingProfileService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BillingProfileResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBillingProfileService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdvertiser(
        AssociationRequest request,
        IBillingProfileService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdvertiser(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdvertiser(
    AssociationRequest request,
    IBillingProfileService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdvertiser(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPaymentMethods(
        MultipleAssociationRequest request,
        IBillingProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPaymentMethods(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPaymentMethods(
        MultipleAssociationRequest request,
        IBillingProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPaymentMethods(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAdAccounts(
        MultipleAssociationRequest request,
        IBillingProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAdAccounts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAdAccounts(
        MultipleAssociationRequest request,
        IBillingProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAdAccounts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static BillingProfile mapRequestToBillingProfile( BillingProfileRequest request ) {
        var model = new BillingProfile
        {
            Id = request.Id,
            BillingName = request.BillingName,
            TaxId = request.TaxId,
            BillingAddress = request.BillingAddress,
            PaymentTerms = request.PaymentTerms,
        };
        return model;
    }

}
