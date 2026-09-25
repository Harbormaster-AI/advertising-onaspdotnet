
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class PublisherEndpoints
{
    public static IEndpointRouteBuilder MapPublisherEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/publisher").WithTags("Publishers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToInventorySources", AddToInventorySources);
    group.MapPut("/removeFromInventorySources", RemoveFromInventorySources);

    group.MapPut("/addToDeals", AddToDeals);
    group.MapPut("/removeFromDeals", RemoveFromDeals);

    group.MapPut("/addToCreativeApprovals", AddToCreativeApprovals);
    group.MapPut("/removeFromCreativeApprovals", RemoveFromCreativeApprovals);

    group.MapPut("/addToInsertionOrders", AddToInsertionOrders);
    group.MapPut("/removeFromInsertionOrders", RemoveFromInsertionOrders);

    group.MapPut("/addToRateCards", AddToRateCards);
    group.MapPut("/removeFromRateCards", RemoveFromRateCards);


        return app;
    }

    private static async Task<IResult> Create(
        PublisherRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPublisher( request );

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
        PublisherRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPublisher( request );

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
        IPublisherService service,
        CancellationToken cancellationToken) {

        var publisher = await service.Get(identifier, cancellationToken);
        return publisher is null ? Results.NotFound() : Results.Ok( publisher );
    }


    private static async Task<IResult> GetAll(
        IPublisherService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PublisherResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToInventorySources(
        MultipleAssociationRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInventorySources(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInventorySources(
        MultipleAssociationRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInventorySources(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDeals(
        MultipleAssociationRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDeals(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDeals(
        MultipleAssociationRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDeals(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCreativeApprovals(
        MultipleAssociationRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCreativeApprovals(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCreativeApprovals(
        MultipleAssociationRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCreativeApprovals(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInsertionOrders(
        MultipleAssociationRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInsertionOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInsertionOrders(
        MultipleAssociationRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInsertionOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRateCards(
        MultipleAssociationRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRateCards(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRateCards(
        MultipleAssociationRequest request,
        IPublisherService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRateCards(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Publisher mapRequestToPublisher( PublisherRequest request ) {
        var model = new Publisher
        {
            Id = request.Id,
            Name = request.Name,
            Website = request.Website,
            PublisherType = request.PublisherType,
        };
        return model;
    }

}
