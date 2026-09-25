
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class LineItemEndpoints
{
    public static IEndpointRouteBuilder MapLineItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/lineItem").WithTags("LineItems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCampaign", AssignCampaign);
        group.MapPut("/unassignCampaign", UnassignCampaign);
        group.MapPut("/assignTargetingProfile", AssignTargetingProfile);
        group.MapPut("/unassignTargetingProfile", UnassignTargetingProfile);
        group.MapPut("/assignDeal", AssignDeal);
        group.MapPut("/unassignDeal", UnassignDeal);

    group.MapPut("/addToPlacements", AddToPlacements);
    group.MapPut("/removeFromPlacements", RemoveFromPlacements);

    group.MapPut("/addToCreatives", AddToCreatives);
    group.MapPut("/removeFromCreatives", RemoveFromCreatives);

    group.MapPut("/addToPerformanceMetrics", AddToPerformanceMetrics);
    group.MapPut("/removeFromPerformanceMetrics", RemoveFromPerformanceMetrics);


        return app;
    }

    private static async Task<IResult> Create(
        LineItemRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLineItem( request );

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
        LineItemRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLineItem( request );

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
        ILineItemService service,
        CancellationToken cancellationToken) {

        var lineItem = await service.Get(identifier, cancellationToken);
        return lineItem is null ? Results.NotFound() : Results.Ok( lineItem );
    }


    private static async Task<IResult> GetAll(
        ILineItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LineItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILineItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCampaign(
        AssociationRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCampaign(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCampaign(
    AssociationRequest request,
    ILineItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCampaign(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTargetingProfile(
        AssociationRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTargetingProfile(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTargetingProfile(
    AssociationRequest request,
    ILineItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTargetingProfile(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeal(
        AssociationRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDeal(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDeal(
    AssociationRequest request,
    ILineItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDeal(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPlacements(
        MultipleAssociationRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPlacements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPlacements(
        MultipleAssociationRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPlacements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCreatives(
        MultipleAssociationRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCreatives(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCreatives(
        MultipleAssociationRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCreatives(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPerformanceMetrics(
        MultipleAssociationRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPerformanceMetrics(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPerformanceMetrics(
        MultipleAssociationRequest request,
        ILineItemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPerformanceMetrics(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static LineItem mapRequestToLineItem( LineItemRequest request ) {
        var model = new LineItem
        {
            Id = request.Id,
            Name = request.Name,
            BidAmount = request.BidAmount,
            DailyBudget = request.DailyBudget,
            FrequencyCap = request.FrequencyCap,
            Status = request.Status,
            PricingModel = request.PricingModel,
            BidStrategy = request.BidStrategy,
            Pacing = request.Pacing,
        };
        return model;
    }

}
