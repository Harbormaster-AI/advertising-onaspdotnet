
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class RateCardEndpoints
{
    public static IEndpointRouteBuilder MapRateCardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/rateCard").WithTags("RateCards");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPublisher", AssignPublisher);
        group.MapPut("/unassignPublisher", UnassignPublisher);

    group.MapPut("/addToRates", AddToRates);
    group.MapPut("/removeFromRates", RemoveFromRates);


        return app;
    }

    private static async Task<IResult> Create(
        RateCardRequest request,
        IRateCardService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRateCard( request );

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
        RateCardRequest request,
        IRateCardService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRateCard( request );

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
        IRateCardService service,
        CancellationToken cancellationToken) {

        var rateCard = await service.Get(identifier, cancellationToken);
        return rateCard is null ? Results.NotFound() : Results.Ok( rateCard );
    }


    private static async Task<IResult> GetAll(
        IRateCardService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RateCardResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRateCardService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPublisher(
        AssociationRequest request,
        IRateCardService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPublisher(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPublisher(
    AssociationRequest request,
    IRateCardService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPublisher(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRates(
        MultipleAssociationRequest request,
        IRateCardService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRates(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRates(
        MultipleAssociationRequest request,
        IRateCardService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRates(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static RateCard mapRequestToRateCard( RateCardRequest request ) {
        var model = new RateCard
        {
            Id = request.Id,
            Name = request.Name,
            EffectiveDate = request.EffectiveDate,
            Currency = request.Currency,
        };
        return model;
    }

}
