
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class PerformanceMetricEndpoints
{
    public static IEndpointRouteBuilder MapPerformanceMetricEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/performanceMetric").WithTags("PerformanceMetrics");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAdAccount", AssignAdAccount);
        group.MapPut("/unassignAdAccount", UnassignAdAccount);
        group.MapPut("/assignCampaign", AssignCampaign);
        group.MapPut("/unassignCampaign", UnassignCampaign);
        group.MapPut("/assignLineItem", AssignLineItem);
        group.MapPut("/unassignLineItem", UnassignLineItem);
        group.MapPut("/assignPlacement", AssignPlacement);
        group.MapPut("/unassignPlacement", UnassignPlacement);
        group.MapPut("/assignCreativeAsset", AssignCreativeAsset);
        group.MapPut("/unassignCreativeAsset", UnassignCreativeAsset);


        return app;
    }

    private static async Task<IResult> Create(
        PerformanceMetricRequest request,
        IPerformanceMetricService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPerformanceMetric( request );

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
        PerformanceMetricRequest request,
        IPerformanceMetricService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPerformanceMetric( request );

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
        IPerformanceMetricService service,
        CancellationToken cancellationToken) {

        var performanceMetric = await service.Get(identifier, cancellationToken);
        return performanceMetric is null ? Results.NotFound() : Results.Ok( performanceMetric );
    }


    private static async Task<IResult> GetAll(
        IPerformanceMetricService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PerformanceMetricResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPerformanceMetricService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdAccount(
        AssociationRequest request,
        IPerformanceMetricService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdAccount(
    AssociationRequest request,
    IPerformanceMetricService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCampaign(
        AssociationRequest request,
        IPerformanceMetricService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCampaign(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCampaign(
    AssociationRequest request,
    IPerformanceMetricService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCampaign(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLineItem(
        AssociationRequest request,
        IPerformanceMetricService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLineItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLineItem(
    AssociationRequest request,
    IPerformanceMetricService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLineItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlacement(
        AssociationRequest request,
        IPerformanceMetricService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlacement(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlacement(
    AssociationRequest request,
    IPerformanceMetricService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlacement(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCreativeAsset(
        AssociationRequest request,
        IPerformanceMetricService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCreativeAsset(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCreativeAsset(
    AssociationRequest request,
    IPerformanceMetricService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCreativeAsset(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static PerformanceMetric mapRequestToPerformanceMetric( PerformanceMetricRequest request ) {
        var model = new PerformanceMetric
        {
            Id = request.Id,
            Date = request.Date,
            Value = request.Value,
            MetricType = request.MetricType,
        };
        return model;
    }

}
