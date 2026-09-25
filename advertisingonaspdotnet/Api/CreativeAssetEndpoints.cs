
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class CreativeAssetEndpoints
{
    public static IEndpointRouteBuilder MapCreativeAssetEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/creativeAsset").WithTags("CreativeAssets");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToFiles", AddToFiles);
    group.MapPut("/removeFromFiles", RemoveFromFiles);

    group.MapPut("/addToApprovals", AddToApprovals);
    group.MapPut("/removeFromApprovals", RemoveFromApprovals);

    group.MapPut("/addToVariations", AddToVariations);
    group.MapPut("/removeFromVariations", RemoveFromVariations);

    group.MapPut("/addToLineItems", AddToLineItems);
    group.MapPut("/removeFromLineItems", RemoveFromLineItems);


        return app;
    }

    private static async Task<IResult> Create(
        CreativeAssetRequest request,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCreativeAsset( request );

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
        CreativeAssetRequest request,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCreativeAsset( request );

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
        ICreativeAssetService service,
        CancellationToken cancellationToken) {

        var creativeAsset = await service.Get(identifier, cancellationToken);
        return creativeAsset is null ? Results.NotFound() : Results.Ok( creativeAsset );
    }


    private static async Task<IResult> GetAll(
        ICreativeAssetService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CreativeAssetResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToFiles(
        MultipleAssociationRequest request,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFiles(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFiles(
        MultipleAssociationRequest request,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFiles(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToApprovals(
        MultipleAssociationRequest request,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToApprovals(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromApprovals(
        MultipleAssociationRequest request,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromApprovals(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToVariations(
        MultipleAssociationRequest request,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToVariations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromVariations(
        MultipleAssociationRequest request,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromVariations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLineItems(
        MultipleAssociationRequest request,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLineItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLineItems(
        MultipleAssociationRequest request,
        ICreativeAssetService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLineItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static CreativeAsset mapRequestToCreativeAsset( CreativeAssetRequest request ) {
        var model = new CreativeAsset
        {
            Id = request.Id,
            Name = request.Name,
            ClickUrl = request.ClickUrl,
            LandingPage = request.LandingPage,
            Width = request.Width,
            Height = request.Height,
            DurationSeconds = request.DurationSeconds,
            CreativeType = request.CreativeType,
            AdFormat = request.AdFormat,
        };
        return model;
    }

}
