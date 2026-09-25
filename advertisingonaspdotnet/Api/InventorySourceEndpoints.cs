
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class InventorySourceEndpoints
{
    public static IEndpointRouteBuilder MapInventorySourceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inventorySource").WithTags("InventorySources");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPublisher", AssignPublisher);
        group.MapPut("/unassignPublisher", UnassignPublisher);

    group.MapPut("/addToAdSlots", AddToAdSlots);
    group.MapPut("/removeFromAdSlots", RemoveFromAdSlots);

    group.MapPut("/addToDeals", AddToDeals);
    group.MapPut("/removeFromDeals", RemoveFromDeals);


        return app;
    }

    private static async Task<IResult> Create(
        InventorySourceRequest request,
        IInventorySourceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInventorySource( request );

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
        InventorySourceRequest request,
        IInventorySourceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInventorySource( request );

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
        IInventorySourceService service,
        CancellationToken cancellationToken) {

        var inventorySource = await service.Get(identifier, cancellationToken);
        return inventorySource is null ? Results.NotFound() : Results.Ok( inventorySource );
    }


    private static async Task<IResult> GetAll(
        IInventorySourceService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InventorySourceResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInventorySourceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPublisher(
        AssociationRequest request,
        IInventorySourceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPublisher(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPublisher(
    AssociationRequest request,
    IInventorySourceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPublisher(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAdSlots(
        MultipleAssociationRequest request,
        IInventorySourceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAdSlots(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAdSlots(
        MultipleAssociationRequest request,
        IInventorySourceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAdSlots(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDeals(
        MultipleAssociationRequest request,
        IInventorySourceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDeals(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDeals(
        MultipleAssociationRequest request,
        IInventorySourceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDeals(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static InventorySource mapRequestToInventorySource( InventorySourceRequest request ) {
        var model = new InventorySource
        {
            Id = request.Id,
            Name = request.Name,
            Domain = request.Domain,
            Channel = request.Channel,
            PrimaryFormat = request.PrimaryFormat,
        };
        return model;
    }

}
