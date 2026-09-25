
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class InsertionOrderEndpoints
{
    public static IEndpointRouteBuilder MapInsertionOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/insertionOrder").WithTags("InsertionOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAdvertiser", AssignAdvertiser);
        group.MapPut("/unassignAdvertiser", UnassignAdvertiser);
        group.MapPut("/assignAgency", AssignAgency);
        group.MapPut("/unassignAgency", UnassignAgency);
        group.MapPut("/assignPublisher", AssignPublisher);
        group.MapPut("/unassignPublisher", UnassignPublisher);

    group.MapPut("/addToCampaigns", AddToCampaigns);
    group.MapPut("/removeFromCampaigns", RemoveFromCampaigns);


        return app;
    }

    private static async Task<IResult> Create(
        InsertionOrderRequest request,
        IInsertionOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsertionOrder( request );

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
        InsertionOrderRequest request,
        IInsertionOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsertionOrder( request );

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
        IInsertionOrderService service,
        CancellationToken cancellationToken) {

        var insertionOrder = await service.Get(identifier, cancellationToken);
        return insertionOrder is null ? Results.NotFound() : Results.Ok( insertionOrder );
    }


    private static async Task<IResult> GetAll(
        IInsertionOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InsertionOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInsertionOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdvertiser(
        AssociationRequest request,
        IInsertionOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdvertiser(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdvertiser(
    AssociationRequest request,
    IInsertionOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdvertiser(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAgency(
        AssociationRequest request,
        IInsertionOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAgency(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAgency(
    AssociationRequest request,
    IInsertionOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAgency(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPublisher(
        AssociationRequest request,
        IInsertionOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPublisher(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPublisher(
    AssociationRequest request,
    IInsertionOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPublisher(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCampaigns(
        MultipleAssociationRequest request,
        IInsertionOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCampaigns(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCampaigns(
        MultipleAssociationRequest request,
        IInsertionOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCampaigns(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static InsertionOrder mapRequestToInsertionOrder( InsertionOrderRequest request ) {
        var model = new InsertionOrder
        {
            Id = request.Id,
            IoNumber = request.IoNumber,
            AgreedBudget = request.AgreedBudget,
            Flight = request.Flight,
            Status = request.Status,
        };
        return model;
    }

}
