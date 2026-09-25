
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class AudienceSegmentEndpoints
{
    public static IEndpointRouteBuilder MapAudienceSegmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/audienceSegment").WithTags("AudienceSegments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignProvider", AssignProvider);
        group.MapPut("/unassignProvider", UnassignProvider);

    group.MapPut("/addToCampaigns", AddToCampaigns);
    group.MapPut("/removeFromCampaigns", RemoveFromCampaigns);


        return app;
    }

    private static async Task<IResult> Create(
        AudienceSegmentRequest request,
        IAudienceSegmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAudienceSegment( request );

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
        AudienceSegmentRequest request,
        IAudienceSegmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAudienceSegment( request );

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
        IAudienceSegmentService service,
        CancellationToken cancellationToken) {

        var audienceSegment = await service.Get(identifier, cancellationToken);
        return audienceSegment is null ? Results.NotFound() : Results.Ok( audienceSegment );
    }


    private static async Task<IResult> GetAll(
        IAudienceSegmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AudienceSegmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAudienceSegmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProvider(
        AssociationRequest request,
        IAudienceSegmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProvider(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProvider(
    AssociationRequest request,
    IAudienceSegmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProvider(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCampaigns(
        MultipleAssociationRequest request,
        IAudienceSegmentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCampaigns(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCampaigns(
        MultipleAssociationRequest request,
        IAudienceSegmentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCampaigns(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AudienceSegment mapRequestToAudienceSegment( AudienceSegmentRequest request ) {
        var model = new AudienceSegment
        {
            Id = request.Id,
            Name = request.Name,
            EstimatedReach = request.EstimatedReach,
            Description = request.Description,
            ProviderType = request.ProviderType,
        };
        return model;
    }

}
