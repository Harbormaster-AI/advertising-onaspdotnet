
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class DeviceCriterionEndpoints
{
    public static IEndpointRouteBuilder MapDeviceCriterionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceCriterion").WithTags("DeviceCriterions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignTargetingProfile", AssignTargetingProfile);
        group.MapPut("/unassignTargetingProfile", UnassignTargetingProfile);


        return app;
    }

    private static async Task<IResult> Create(
        DeviceCriterionRequest request,
        IDeviceCriterionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDeviceCriterion( request );

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
        DeviceCriterionRequest request,
        IDeviceCriterionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDeviceCriterion( request );

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
        IDeviceCriterionService service,
        CancellationToken cancellationToken) {

        var deviceCriterion = await service.Get(identifier, cancellationToken);
        return deviceCriterion is null ? Results.NotFound() : Results.Ok( deviceCriterion );
    }


    private static async Task<IResult> GetAll(
        IDeviceCriterionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DeviceCriterionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDeviceCriterionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTargetingProfile(
        AssociationRequest request,
        IDeviceCriterionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTargetingProfile(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTargetingProfile(
    AssociationRequest request,
    IDeviceCriterionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTargetingProfile(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static DeviceCriterion mapRequestToDeviceCriterion( DeviceCriterionRequest request ) {
        var model = new DeviceCriterion
        {
            Id = request.Id,
            DeviceType = request.DeviceType,
            PlatformType = request.PlatformType,
            Operator_ = request.Operator_,
        };
        return model;
    }

}
