
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class DataProviderEndpoints
{
    public static IEndpointRouteBuilder MapDataProviderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dataProvider").WithTags("DataProviders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToAudienceSegments", AddToAudienceSegments);
    group.MapPut("/removeFromAudienceSegments", RemoveFromAudienceSegments);


        return app;
    }

    private static async Task<IResult> Create(
        DataProviderRequest request,
        IDataProviderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDataProvider( request );

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
        DataProviderRequest request,
        IDataProviderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDataProvider( request );

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
        IDataProviderService service,
        CancellationToken cancellationToken) {

        var dataProvider = await service.Get(identifier, cancellationToken);
        return dataProvider is null ? Results.NotFound() : Results.Ok( dataProvider );
    }


    private static async Task<IResult> GetAll(
        IDataProviderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DataProviderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDataProviderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAudienceSegments(
        MultipleAssociationRequest request,
        IDataProviderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAudienceSegments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAudienceSegments(
        MultipleAssociationRequest request,
        IDataProviderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAudienceSegments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static DataProvider mapRequestToDataProvider( DataProviderRequest request ) {
        var model = new DataProvider
        {
            Id = request.Id,
            Name = request.Name,
            Website = request.Website,
            ProviderType = request.ProviderType,
        };
        return model;
    }

}
