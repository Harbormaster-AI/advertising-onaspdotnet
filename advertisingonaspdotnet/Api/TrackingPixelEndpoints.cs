
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class TrackingPixelEndpoints
{
    public static IEndpointRouteBuilder MapTrackingPixelEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/trackingPixel").WithTags("TrackingPixels");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCampaign", AssignCampaign);
        group.MapPut("/unassignCampaign", UnassignCampaign);
        group.MapPut("/assignAdvertiser", AssignAdvertiser);
        group.MapPut("/unassignAdvertiser", UnassignAdvertiser);

    group.MapPut("/addToConversionEvents", AddToConversionEvents);
    group.MapPut("/removeFromConversionEvents", RemoveFromConversionEvents);


        return app;
    }

    private static async Task<IResult> Create(
        TrackingPixelRequest request,
        ITrackingPixelService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTrackingPixel( request );

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
        TrackingPixelRequest request,
        ITrackingPixelService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTrackingPixel( request );

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
        ITrackingPixelService service,
        CancellationToken cancellationToken) {

        var trackingPixel = await service.Get(identifier, cancellationToken);
        return trackingPixel is null ? Results.NotFound() : Results.Ok( trackingPixel );
    }


    private static async Task<IResult> GetAll(
        ITrackingPixelService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TrackingPixelResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITrackingPixelService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCampaign(
        AssociationRequest request,
        ITrackingPixelService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCampaign(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCampaign(
    AssociationRequest request,
    ITrackingPixelService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCampaign(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdvertiser(
        AssociationRequest request,
        ITrackingPixelService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdvertiser(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdvertiser(
    AssociationRequest request,
    ITrackingPixelService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdvertiser(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToConversionEvents(
        MultipleAssociationRequest request,
        ITrackingPixelService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToConversionEvents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromConversionEvents(
        MultipleAssociationRequest request,
        ITrackingPixelService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromConversionEvents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static TrackingPixel mapRequestToTrackingPixel( TrackingPixelRequest request ) {
        var model = new TrackingPixel
        {
            Id = request.Id,
            Name = request.Name,
            Url = request.Url,
            EventType = request.EventType,
            PixelType = request.PixelType,
        };
        return model;
    }

}
