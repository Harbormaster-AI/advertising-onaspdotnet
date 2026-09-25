
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class CreativeVariationEndpoints
{
    public static IEndpointRouteBuilder MapCreativeVariationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/creativeVariation").WithTags("CreativeVariations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCreativeAsset", AssignCreativeAsset);
        group.MapPut("/unassignCreativeAsset", UnassignCreativeAsset);


        return app;
    }

    private static async Task<IResult> Create(
        CreativeVariationRequest request,
        ICreativeVariationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCreativeVariation( request );

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
        CreativeVariationRequest request,
        ICreativeVariationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCreativeVariation( request );

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
        ICreativeVariationService service,
        CancellationToken cancellationToken) {

        var creativeVariation = await service.Get(identifier, cancellationToken);
        return creativeVariation is null ? Results.NotFound() : Results.Ok( creativeVariation );
    }


    private static async Task<IResult> GetAll(
        ICreativeVariationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CreativeVariationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICreativeVariationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCreativeAsset(
        AssociationRequest request,
        ICreativeVariationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCreativeAsset(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCreativeAsset(
    AssociationRequest request,
    ICreativeVariationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCreativeAsset(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CreativeVariation mapRequestToCreativeVariation( CreativeVariationRequest request ) {
        var model = new CreativeVariation
        {
            Id = request.Id,
            Name = request.Name,
            Language = request.Language,
            Headline = request.Headline,
            BodyText = request.BodyText,
            CallToAction = request.CallToAction,
        };
        return model;
    }

}
