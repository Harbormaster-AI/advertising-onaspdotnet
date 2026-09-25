
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class AgencyEndpoints
{
    public static IEndpointRouteBuilder MapAgencyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/agency").WithTags("Agencys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToAdvertisers", AddToAdvertisers);
    group.MapPut("/removeFromAdvertisers", RemoveFromAdvertisers);

    group.MapPut("/addToTeams", AddToTeams);
    group.MapPut("/removeFromTeams", RemoveFromTeams);

    group.MapPut("/addToUsers", AddToUsers);
    group.MapPut("/removeFromUsers", RemoveFromUsers);

    group.MapPut("/addToInsertionOrders", AddToInsertionOrders);
    group.MapPut("/removeFromInsertionOrders", RemoveFromInsertionOrders);


        return app;
    }

    private static async Task<IResult> Create(
        AgencyRequest request,
        IAgencyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAgency( request );

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
        AgencyRequest request,
        IAgencyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAgency( request );

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
        IAgencyService service,
        CancellationToken cancellationToken) {

        var agency = await service.Get(identifier, cancellationToken);
        return agency is null ? Results.NotFound() : Results.Ok( agency );
    }


    private static async Task<IResult> GetAll(
        IAgencyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AgencyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAgencyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAdvertisers(
        MultipleAssociationRequest request,
        IAgencyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAdvertisers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAdvertisers(
        MultipleAssociationRequest request,
        IAgencyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAdvertisers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTeams(
        MultipleAssociationRequest request,
        IAgencyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTeams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTeams(
        MultipleAssociationRequest request,
        IAgencyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTeams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToUsers(
        MultipleAssociationRequest request,
        IAgencyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToUsers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromUsers(
        MultipleAssociationRequest request,
        IAgencyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromUsers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInsertionOrders(
        MultipleAssociationRequest request,
        IAgencyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInsertionOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInsertionOrders(
        MultipleAssociationRequest request,
        IAgencyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInsertionOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Agency mapRequestToAgency( AgencyRequest request ) {
        var model = new Agency
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,
        };
        return model;
    }

}
