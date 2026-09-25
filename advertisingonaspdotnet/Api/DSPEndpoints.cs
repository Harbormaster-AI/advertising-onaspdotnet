
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class DSPEndpoints
{
    public static IEndpointRouteBuilder MapDSPEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dSP").WithTags("DSPs");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToAdAccounts", AddToAdAccounts);
    group.MapPut("/removeFromAdAccounts", RemoveFromAdAccounts);


        return app;
    }

    private static async Task<IResult> Create(
        DSPRequest request,
        IDSPService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDSP( request );

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
        DSPRequest request,
        IDSPService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDSP( request );

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
        IDSPService service,
        CancellationToken cancellationToken) {

        var dSP = await service.Get(identifier, cancellationToken);
        return dSP is null ? Results.NotFound() : Results.Ok( dSP );
    }


    private static async Task<IResult> GetAll(
        IDSPService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DSPResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDSPService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAdAccounts(
        MultipleAssociationRequest request,
        IDSPService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAdAccounts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAdAccounts(
        MultipleAssociationRequest request,
        IDSPService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAdAccounts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static DSP mapRequestToDSP( DSPRequest request ) {
        var model = new DSP
        {
            Id = request.Id,
            Name = request.Name,
            Website = request.Website,
            Region = request.Region,
        };
        return model;
    }

}
