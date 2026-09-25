
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class ExperimentVariantEndpoints
{
    public static IEndpointRouteBuilder MapExperimentVariantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/experimentVariant").WithTags("ExperimentVariants");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignExperiment", AssignExperiment);
        group.MapPut("/unassignExperiment", UnassignExperiment);
        group.MapPut("/assignCreativeVariation", AssignCreativeVariation);
        group.MapPut("/unassignCreativeVariation", UnassignCreativeVariation);
        group.MapPut("/assignLineItem", AssignLineItem);
        group.MapPut("/unassignLineItem", UnassignLineItem);


        return app;
    }

    private static async Task<IResult> Create(
        ExperimentVariantRequest request,
        IExperimentVariantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToExperimentVariant( request );

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
        ExperimentVariantRequest request,
        IExperimentVariantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToExperimentVariant( request );

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
        IExperimentVariantService service,
        CancellationToken cancellationToken) {

        var experimentVariant = await service.Get(identifier, cancellationToken);
        return experimentVariant is null ? Results.NotFound() : Results.Ok( experimentVariant );
    }


    private static async Task<IResult> GetAll(
        IExperimentVariantService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ExperimentVariantResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IExperimentVariantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignExperiment(
        AssociationRequest request,
        IExperimentVariantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignExperiment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignExperiment(
    AssociationRequest request,
    IExperimentVariantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignExperiment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCreativeVariation(
        AssociationRequest request,
        IExperimentVariantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCreativeVariation(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCreativeVariation(
    AssociationRequest request,
    IExperimentVariantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCreativeVariation(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLineItem(
        AssociationRequest request,
        IExperimentVariantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLineItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLineItem(
    AssociationRequest request,
    IExperimentVariantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLineItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ExperimentVariant mapRequestToExperimentVariant( ExperimentVariantRequest request ) {
        var model = new ExperimentVariant
        {
            Id = request.Id,
            Name = request.Name,
            Allocation = request.Allocation,
        };
        return model;
    }

}
