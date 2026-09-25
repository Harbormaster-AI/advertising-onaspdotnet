
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class ContentCategoryEndpoints
{
    public static IEndpointRouteBuilder MapContentCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/contentCategory").WithTags("ContentCategorys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);



        return app;
    }

    private static async Task<IResult> Create(
        ContentCategoryRequest request,
        IContentCategoryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToContentCategory( request );

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
        ContentCategoryRequest request,
        IContentCategoryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToContentCategory( request );

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
        IContentCategoryService service,
        CancellationToken cancellationToken) {

        var contentCategory = await service.Get(identifier, cancellationToken);
        return contentCategory is null ? Results.NotFound() : Results.Ok( contentCategory );
    }


    private static async Task<IResult> GetAll(
        IContentCategoryService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ContentCategoryResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IContentCategoryService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static ContentCategory mapRequestToContentCategory( ContentCategoryRequest request ) {
        var model = new ContentCategory
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
        };
        return model;
    }

}
