
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class CreativeFileEndpoints
{
    public static IEndpointRouteBuilder MapCreativeFileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/creativeFile").WithTags("CreativeFiles");

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
        CreativeFileRequest request,
        ICreativeFileService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCreativeFile( request );

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
        CreativeFileRequest request,
        ICreativeFileService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCreativeFile( request );

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
        ICreativeFileService service,
        CancellationToken cancellationToken) {

        var creativeFile = await service.Get(identifier, cancellationToken);
        return creativeFile is null ? Results.NotFound() : Results.Ok( creativeFile );
    }


    private static async Task<IResult> GetAll(
        ICreativeFileService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CreativeFileResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICreativeFileService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCreativeAsset(
        AssociationRequest request,
        ICreativeFileService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCreativeAsset(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCreativeAsset(
    AssociationRequest request,
    ICreativeFileService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCreativeAsset(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CreativeFile mapRequestToCreativeFile( CreativeFileRequest request ) {
        var model = new CreativeFile
        {
            Id = request.Id,
            Uri = request.Uri,
            FileSizeKB = request.FileSizeKB,
            MimeType = request.MimeType,
            Checksum = request.Checksum,
        };
        return model;
    }

}
