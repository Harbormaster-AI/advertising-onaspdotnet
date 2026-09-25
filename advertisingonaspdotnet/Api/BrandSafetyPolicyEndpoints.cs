
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class BrandSafetyPolicyEndpoints
{
    public static IEndpointRouteBuilder MapBrandSafetyPolicyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/brandSafetyPolicy").WithTags("BrandSafetyPolicys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToTargetingProfiles", AddToTargetingProfiles);
    group.MapPut("/removeFromTargetingProfiles", RemoveFromTargetingProfiles);


        return app;
    }

    private static async Task<IResult> Create(
        BrandSafetyPolicyRequest request,
        IBrandSafetyPolicyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBrandSafetyPolicy( request );

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
        BrandSafetyPolicyRequest request,
        IBrandSafetyPolicyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBrandSafetyPolicy( request );

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
        IBrandSafetyPolicyService service,
        CancellationToken cancellationToken) {

        var brandSafetyPolicy = await service.Get(identifier, cancellationToken);
        return brandSafetyPolicy is null ? Results.NotFound() : Results.Ok( brandSafetyPolicy );
    }


    private static async Task<IResult> GetAll(
        IBrandSafetyPolicyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BrandSafetyPolicyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBrandSafetyPolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToTargetingProfiles(
        MultipleAssociationRequest request,
        IBrandSafetyPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTargetingProfiles(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTargetingProfiles(
        MultipleAssociationRequest request,
        IBrandSafetyPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTargetingProfiles(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static BrandSafetyPolicy mapRequestToBrandSafetyPolicy( BrandSafetyPolicyRequest request ) {
        var model = new BrandSafetyPolicy
        {
            Id = request.Id,
            Level = request.Level,
            ContentRatingThreshold = request.ContentRatingThreshold,
        };
        return model;
    }

}
