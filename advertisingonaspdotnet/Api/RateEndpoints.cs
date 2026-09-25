
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class RateEndpoints
{
    public static IEndpointRouteBuilder MapRateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/rate").WithTags("Rates");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRateCard", AssignRateCard);
        group.MapPut("/unassignRateCard", UnassignRateCard);
        group.MapPut("/assignAdSlot", AssignAdSlot);
        group.MapPut("/unassignAdSlot", UnassignAdSlot);


        return app;
    }

    private static async Task<IResult> Create(
        RateRequest request,
        IRateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRate( request );

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
        RateRequest request,
        IRateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRate( request );

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
        IRateService service,
        CancellationToken cancellationToken) {

        var rate = await service.Get(identifier, cancellationToken);
        return rate is null ? Results.NotFound() : Results.Ok( rate );
    }


    private static async Task<IResult> GetAll(
        IRateService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RateResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRateCard(
        AssociationRequest request,
        IRateService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRateCard(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRateCard(
    AssociationRequest request,
    IRateService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRateCard(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdSlot(
        AssociationRequest request,
        IRateService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdSlot(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdSlot(
    AssociationRequest request,
    IRateService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdSlot(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Rate mapRequestToRate( RateRequest request ) {
        var model = new Rate
        {
            Id = request.Id,
            UnitPrice = request.UnitPrice,
            AdFormat = request.AdFormat,
            PricingModel = request.PricingModel,
        };
        return model;
    }

}
