
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class CampaignEndpoints
{
    public static IEndpointRouteBuilder MapCampaignEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/campaign").WithTags("Campaigns");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAdAccount", AssignAdAccount);
        group.MapPut("/unassignAdAccount", UnassignAdAccount);
        group.MapPut("/assignInsertionOrder", AssignInsertionOrder);
        group.MapPut("/unassignInsertionOrder", UnassignInsertionOrder);

    group.MapPut("/addToLineItems", AddToLineItems);
    group.MapPut("/removeFromLineItems", RemoveFromLineItems);

    group.MapPut("/addToKpis", AddToKpis);
    group.MapPut("/removeFromKpis", RemoveFromKpis);

    group.MapPut("/addToTrackingPixels", AddToTrackingPixels);
    group.MapPut("/removeFromTrackingPixels", RemoveFromTrackingPixels);

    group.MapPut("/addToAudiences", AddToAudiences);
    group.MapPut("/removeFromAudiences", RemoveFromAudiences);

    group.MapPut("/addToReports", AddToReports);
    group.MapPut("/removeFromReports", RemoveFromReports);


        return app;
    }

    private static async Task<IResult> Create(
        CampaignRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCampaign( request );

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
        CampaignRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCampaign( request );

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
        ICampaignService service,
        CancellationToken cancellationToken) {

        var campaign = await service.Get(identifier, cancellationToken);
        return campaign is null ? Results.NotFound() : Results.Ok( campaign );
    }


    private static async Task<IResult> GetAll(
        ICampaignService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CampaignResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdAccount(
        AssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdAccount(
    AssociationRequest request,
    ICampaignService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInsertionOrder(
        AssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInsertionOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInsertionOrder(
    AssociationRequest request,
    ICampaignService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInsertionOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToLineItems(
        MultipleAssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLineItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLineItems(
        MultipleAssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLineItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToKpis(
        MultipleAssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToKpis(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromKpis(
        MultipleAssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromKpis(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTrackingPixels(
        MultipleAssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTrackingPixels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTrackingPixels(
        MultipleAssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTrackingPixels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAudiences(
        MultipleAssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAudiences(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAudiences(
        MultipleAssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAudiences(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToReports(
        MultipleAssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToReports(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromReports(
        MultipleAssociationRequest request,
        ICampaignService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromReports(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Campaign mapRequestToCampaign( CampaignRequest request ) {
        var model = new Campaign
        {
            Id = request.Id,
            Name = request.Name,
            TotalBudget = request.TotalBudget,
            Flight = request.Flight,
            Objective = request.Objective,
            Status = request.Status,
        };
        return model;
    }

}
