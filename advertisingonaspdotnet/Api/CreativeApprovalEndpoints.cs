
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class CreativeApprovalEndpoints
{
    public static IEndpointRouteBuilder MapCreativeApprovalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/creativeApproval").WithTags("CreativeApprovals");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCreativeAsset", AssignCreativeAsset);
        group.MapPut("/unassignCreativeAsset", UnassignCreativeAsset);
        group.MapPut("/assignPublisher", AssignPublisher);
        group.MapPut("/unassignPublisher", UnassignPublisher);


        return app;
    }

    private static async Task<IResult> Create(
        CreativeApprovalRequest request,
        ICreativeApprovalService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCreativeApproval( request );

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
        CreativeApprovalRequest request,
        ICreativeApprovalService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCreativeApproval( request );

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
        ICreativeApprovalService service,
        CancellationToken cancellationToken) {

        var creativeApproval = await service.Get(identifier, cancellationToken);
        return creativeApproval is null ? Results.NotFound() : Results.Ok( creativeApproval );
    }


    private static async Task<IResult> GetAll(
        ICreativeApprovalService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CreativeApprovalResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICreativeApprovalService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCreativeAsset(
        AssociationRequest request,
        ICreativeApprovalService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCreativeAsset(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCreativeAsset(
    AssociationRequest request,
    ICreativeApprovalService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCreativeAsset(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPublisher(
        AssociationRequest request,
        ICreativeApprovalService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPublisher(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPublisher(
    AssociationRequest request,
    ICreativeApprovalService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPublisher(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CreativeApproval mapRequestToCreativeApproval( CreativeApprovalRequest request ) {
        var model = new CreativeApproval
        {
            Id = request.Id,
            Reviewer = request.Reviewer,
            ReviewedAt = request.ReviewedAt,
            Status = request.Status,
        };
        return model;
    }

}
