
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class TargetingProfileEndpoints
{
    public static IEndpointRouteBuilder MapTargetingProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/targetingProfile").WithTags("TargetingProfiles");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignBrandSafetyPolicy", AssignBrandSafetyPolicy);
        group.MapPut("/unassignBrandSafetyPolicy", UnassignBrandSafetyPolicy);

    group.MapPut("/addToAudienceSegments", AddToAudienceSegments);
    group.MapPut("/removeFromAudienceSegments", RemoveFromAudienceSegments);

    group.MapPut("/addToGeoRegions", AddToGeoRegions);
    group.MapPut("/removeFromGeoRegions", RemoveFromGeoRegions);

    group.MapPut("/addToContentCategories", AddToContentCategories);
    group.MapPut("/removeFromContentCategories", RemoveFromContentCategories);

    group.MapPut("/addToDeviceCriteria", AddToDeviceCriteria);
    group.MapPut("/removeFromDeviceCriteria", RemoveFromDeviceCriteria);


        return app;
    }

    private static async Task<IResult> Create(
        TargetingProfileRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTargetingProfile( request );

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
        TargetingProfileRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTargetingProfile( request );

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
        ITargetingProfileService service,
        CancellationToken cancellationToken) {

        var targetingProfile = await service.Get(identifier, cancellationToken);
        return targetingProfile is null ? Results.NotFound() : Results.Ok( targetingProfile );
    }


    private static async Task<IResult> GetAll(
        ITargetingProfileService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TargetingProfileResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBrandSafetyPolicy(
        AssociationRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBrandSafetyPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBrandSafetyPolicy(
    AssociationRequest request,
    ITargetingProfileService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBrandSafetyPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAudienceSegments(
        MultipleAssociationRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAudienceSegments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAudienceSegments(
        MultipleAssociationRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAudienceSegments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToGeoRegions(
        MultipleAssociationRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToGeoRegions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromGeoRegions(
        MultipleAssociationRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromGeoRegions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToContentCategories(
        MultipleAssociationRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToContentCategories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromContentCategories(
        MultipleAssociationRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromContentCategories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDeviceCriteria(
        MultipleAssociationRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDeviceCriteria(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDeviceCriteria(
        MultipleAssociationRequest request,
        ITargetingProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDeviceCriteria(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static TargetingProfile mapRequestToTargetingProfile( TargetingProfileRequest request ) {
        var model = new TargetingProfile
        {
            Id = request.Id,
            Name = request.Name,
        };
        return model;
    }

}
