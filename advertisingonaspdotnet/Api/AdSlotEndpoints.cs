
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class AdSlotEndpoints
{
    public static IEndpointRouteBuilder MapAdSlotEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/adSlot").WithTags("AdSlots");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInventorySource", AssignInventorySource);
        group.MapPut("/unassignInventorySource", UnassignInventorySource);

    group.MapPut("/addToPlacements", AddToPlacements);
    group.MapPut("/removeFromPlacements", RemoveFromPlacements);

    group.MapPut("/addToRates", AddToRates);
    group.MapPut("/removeFromRates", RemoveFromRates);


        return app;
    }

    private static async Task<IResult> Create(
        AdSlotRequest request,
        IAdSlotService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAdSlot( request );

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
        AdSlotRequest request,
        IAdSlotService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAdSlot( request );

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
        IAdSlotService service,
        CancellationToken cancellationToken) {

        var adSlot = await service.Get(identifier, cancellationToken);
        return adSlot is null ? Results.NotFound() : Results.Ok( adSlot );
    }


    private static async Task<IResult> GetAll(
        IAdSlotService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AdSlotResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAdSlotService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInventorySource(
        AssociationRequest request,
        IAdSlotService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInventorySource(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInventorySource(
    AssociationRequest request,
    IAdSlotService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInventorySource(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPlacements(
        MultipleAssociationRequest request,
        IAdSlotService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPlacements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPlacements(
        MultipleAssociationRequest request,
        IAdSlotService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPlacements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRates(
        MultipleAssociationRequest request,
        IAdSlotService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRates(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRates(
        MultipleAssociationRequest request,
        IAdSlotService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRates(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AdSlot mapRequestToAdSlot( AdSlotRequest request ) {
        var model = new AdSlot
        {
            Id = request.Id,
            SlotCode = request.SlotCode,
            Width = request.Width,
            Height = request.Height,
            FloorPrice = request.FloorPrice,
            Format = request.Format,
        };
        return model;
    }

}
