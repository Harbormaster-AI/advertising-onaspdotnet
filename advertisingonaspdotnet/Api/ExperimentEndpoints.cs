
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class ExperimentEndpoints
{
    public static IEndpointRouteBuilder MapExperimentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/experiment").WithTags("Experiments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCampaign", AssignCampaign);
        group.MapPut("/unassignCampaign", UnassignCampaign);

    group.MapPut("/addToVariants", AddToVariants);
    group.MapPut("/removeFromVariants", RemoveFromVariants);


        return app;
    }

    private static async Task<IResult> Create(
        ExperimentRequest request,
        IExperimentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToExperiment( request );

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
        ExperimentRequest request,
        IExperimentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToExperiment( request );

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
        IExperimentService service,
        CancellationToken cancellationToken) {

        var experiment = await service.Get(identifier, cancellationToken);
        return experiment is null ? Results.NotFound() : Results.Ok( experiment );
    }


    private static async Task<IResult> GetAll(
        IExperimentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ExperimentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IExperimentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCampaign(
        AssociationRequest request,
        IExperimentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCampaign(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCampaign(
    AssociationRequest request,
    IExperimentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCampaign(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToVariants(
        MultipleAssociationRequest request,
        IExperimentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToVariants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromVariants(
        MultipleAssociationRequest request,
        IExperimentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromVariants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Experiment mapRequestToExperiment( ExperimentRequest request ) {
        var model = new Experiment
        {
            Id = request.Id,
            Name = request.Name,
            Hypothesis = request.Hypothesis,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
        };
        return model;
    }

}
