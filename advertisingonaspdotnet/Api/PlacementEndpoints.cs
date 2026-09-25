
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class PlacementEndpoints
{
    public static IEndpointRouteBuilder MapPlacementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/placement").WithTags("Placements");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignLineItem", AssignLineItem);
        group.MapPut("/unassignLineItem", UnassignLineItem);
        group.MapPut("/assignAdSlot", AssignAdSlot);
        group.MapPut("/unassignAdSlot", UnassignAdSlot);
        group.MapPut("/assignDeal", AssignDeal);
        group.MapPut("/unassignDeal", UnassignDeal);


        return app;
    }

    private static async Task<IResult> Create(
        PlacementRequest request,
        IPlacementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPlacement( request );

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
        PlacementRequest request,
        IPlacementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPlacement( request );

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
        IPlacementService service,
        CancellationToken cancellationToken) {

        var placement = await service.Get(identifier, cancellationToken);
        return placement is null ? Results.NotFound() : Results.Ok( placement );
    }


    private static async Task<IResult> GetAll(
        IPlacementService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PlacementResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPlacementService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLineItem(
        AssociationRequest request,
        IPlacementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLineItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLineItem(
    AssociationRequest request,
    IPlacementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLineItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdSlot(
        AssociationRequest request,
        IPlacementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdSlot(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdSlot(
    AssociationRequest request,
    IPlacementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdSlot(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeal(
        AssociationRequest request,
        IPlacementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDeal(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDeal(
    AssociationRequest request,
    IPlacementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDeal(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Placement mapRequestToPlacement( PlacementRequest request ) {
        var model = new Placement
        {
            Id = request.Id,
            Name = request.Name,
            Flight = request.Flight,
            GoalImpressions = request.GoalImpressions,
        };
        return model;
    }

}
