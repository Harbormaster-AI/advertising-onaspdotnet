
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class KPIEndpoints
{
    public static IEndpointRouteBuilder MapKPIEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/kPI").WithTags("KPIs");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCampaign", AssignCampaign);
        group.MapPut("/unassignCampaign", UnassignCampaign);


        return app;
    }

    private static async Task<IResult> Create(
        KPIRequest request,
        IKPIService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToKPI( request );

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
        KPIRequest request,
        IKPIService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToKPI( request );

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
        IKPIService service,
        CancellationToken cancellationToken) {

        var kPI = await service.Get(identifier, cancellationToken);
        return kPI is null ? Results.NotFound() : Results.Ok( kPI );
    }


    private static async Task<IResult> GetAll(
        IKPIService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( KPIResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IKPIService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCampaign(
        AssociationRequest request,
        IKPIService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCampaign(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCampaign(
    AssociationRequest request,
    IKPIService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCampaign(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static KPI mapRequestToKPI( KPIRequest request ) {
        var model = new KPI
        {
            Id = request.Id,
            TargetValue = request.TargetValue,
            MetricType = request.MetricType,
        };
        return model;
    }

}
