
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class DealEndpoints
{
    public static IEndpointRouteBuilder MapDealEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deal").WithTags("Deals");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPublisher", AssignPublisher);
        group.MapPut("/unassignPublisher", UnassignPublisher);

    group.MapPut("/addToInventorySources", AddToInventorySources);
    group.MapPut("/removeFromInventorySources", RemoveFromInventorySources);

    group.MapPut("/addToPlacements", AddToPlacements);
    group.MapPut("/removeFromPlacements", RemoveFromPlacements);


        return app;
    }

    private static async Task<IResult> Create(
        DealRequest request,
        IDealService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDeal( request );

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
        DealRequest request,
        IDealService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDeal( request );

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
        IDealService service,
        CancellationToken cancellationToken) {

        var deal = await service.Get(identifier, cancellationToken);
        return deal is null ? Results.NotFound() : Results.Ok( deal );
    }


    private static async Task<IResult> GetAll(
        IDealService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DealResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDealService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPublisher(
        AssociationRequest request,
        IDealService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPublisher(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPublisher(
    AssociationRequest request,
    IDealService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPublisher(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToInventorySources(
        MultipleAssociationRequest request,
        IDealService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInventorySources(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInventorySources(
        MultipleAssociationRequest request,
        IDealService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInventorySources(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPlacements(
        MultipleAssociationRequest request,
        IDealService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPlacements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPlacements(
        MultipleAssociationRequest request,
        IDealService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPlacements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Deal mapRequestToDeal( DealRequest request ) {
        var model = new Deal
        {
            Id = request.Id,
            FloorPrice = request.FloorPrice,
            DealType = request.DealType,
        };
        return model;
    }

}
