
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class GeoRegionEndpoints
{
    public static IEndpointRouteBuilder MapGeoRegionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/geoRegion").WithTags("GeoRegions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignParent", AssignParent);
        group.MapPut("/unassignParent", UnassignParent);

    group.MapPut("/addToChildren", AddToChildren);
    group.MapPut("/removeFromChildren", RemoveFromChildren);


        return app;
    }

    private static async Task<IResult> Create(
        GeoRegionRequest request,
        IGeoRegionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGeoRegion( request );

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
        GeoRegionRequest request,
        IGeoRegionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGeoRegion( request );

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
        IGeoRegionService service,
        CancellationToken cancellationToken) {

        var geoRegion = await service.Get(identifier, cancellationToken);
        return geoRegion is null ? Results.NotFound() : Results.Ok( geoRegion );
    }


    private static async Task<IResult> GetAll(
        IGeoRegionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( GeoRegionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IGeoRegionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignParent(
        AssociationRequest request,
        IGeoRegionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignParent(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignParent(
    AssociationRequest request,
    IGeoRegionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignParent(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToChildren(
        MultipleAssociationRequest request,
        IGeoRegionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToChildren(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromChildren(
        MultipleAssociationRequest request,
        IGeoRegionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromChildren(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static GeoRegion mapRequestToGeoRegion( GeoRegionRequest request ) {
        var model = new GeoRegion
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
            RegionType = request.RegionType,
        };
        return model;
    }

}
