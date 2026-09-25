
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class ConversionEventEndpoints
{
    public static IEndpointRouteBuilder MapConversionEventEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/conversionEvent").WithTags("ConversionEvents");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCampaign", AssignCampaign);
        group.MapPut("/unassignCampaign", UnassignCampaign);
        group.MapPut("/assignLineItem", AssignLineItem);
        group.MapPut("/unassignLineItem", UnassignLineItem);
        group.MapPut("/assignTrackingPixel", AssignTrackingPixel);
        group.MapPut("/unassignTrackingPixel", UnassignTrackingPixel);


        return app;
    }

    private static async Task<IResult> Create(
        ConversionEventRequest request,
        IConversionEventService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToConversionEvent( request );

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
        ConversionEventRequest request,
        IConversionEventService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToConversionEvent( request );

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
        IConversionEventService service,
        CancellationToken cancellationToken) {

        var conversionEvent = await service.Get(identifier, cancellationToken);
        return conversionEvent is null ? Results.NotFound() : Results.Ok( conversionEvent );
    }


    private static async Task<IResult> GetAll(
        IConversionEventService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ConversionEventResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IConversionEventService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCampaign(
        AssociationRequest request,
        IConversionEventService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCampaign(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCampaign(
    AssociationRequest request,
    IConversionEventService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCampaign(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLineItem(
        AssociationRequest request,
        IConversionEventService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLineItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLineItem(
    AssociationRequest request,
    IConversionEventService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLineItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTrackingPixel(
        AssociationRequest request,
        IConversionEventService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTrackingPixel(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTrackingPixel(
    AssociationRequest request,
    IConversionEventService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTrackingPixel(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ConversionEvent mapRequestToConversionEvent( ConversionEventRequest request ) {
        var model = new ConversionEvent
        {
            Id = request.Id,
            Timestamp = request.Timestamp,
            Value = request.Value,
            EventType = request.EventType,
            AttributionModel = request.AttributionModel,
        };
        return model;
    }

}
