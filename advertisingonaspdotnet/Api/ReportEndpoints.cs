
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class ReportEndpoints
{
    public static IEndpointRouteBuilder MapReportEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/report").WithTags("Reports");

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


        return app;
    }

    private static async Task<IResult> Create(
        ReportRequest request,
        IReportService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToReport( request );

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
        ReportRequest request,
        IReportService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToReport( request );

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
        IReportService service,
        CancellationToken cancellationToken) {

        var report = await service.Get(identifier, cancellationToken);
        return report is null ? Results.NotFound() : Results.Ok( report );
    }


    private static async Task<IResult> GetAll(
        IReportService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ReportResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IReportService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdAccount(
        AssociationRequest request,
        IReportService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdAccount(
    AssociationRequest request,
    IReportService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCampaign(
        AssociationRequest request,
        IReportService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCampaign(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCampaign(
    AssociationRequest request,
    IReportService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCampaign(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLineItem(
        AssociationRequest request,
        IReportService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLineItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLineItem(
    AssociationRequest request,
    IReportService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLineItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Report mapRequestToReport( ReportRequest request ) {
        var model = new Report
        {
            Id = request.Id,
            ReportName = request.ReportName,
            GeneratedAt = request.GeneratedAt,
            FileUrl = request.FileUrl,
            ReportType = request.ReportType,
        };
        return model;
    }

}
