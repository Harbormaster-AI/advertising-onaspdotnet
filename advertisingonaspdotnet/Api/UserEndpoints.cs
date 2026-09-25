
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/user").WithTags("Users");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAgency", AssignAgency);
        group.MapPut("/unassignAgency", UnassignAgency);

    group.MapPut("/addToTeams", AddToTeams);
    group.MapPut("/removeFromTeams", RemoveFromTeams);

    group.MapPut("/addToAdAccounts", AddToAdAccounts);
    group.MapPut("/removeFromAdAccounts", RemoveFromAdAccounts);


        return app;
    }

    private static async Task<IResult> Create(
        UserRequest request,
        IUserService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToUser( request );

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
        UserRequest request,
        IUserService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToUser( request );

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
        IUserService service,
        CancellationToken cancellationToken) {

        var user = await service.Get(identifier, cancellationToken);
        return user is null ? Results.NotFound() : Results.Ok( user );
    }


    private static async Task<IResult> GetAll(
        IUserService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( UserResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IUserService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAgency(
        AssociationRequest request,
        IUserService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAgency(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAgency(
    AssociationRequest request,
    IUserService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAgency(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToTeams(
        MultipleAssociationRequest request,
        IUserService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTeams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTeams(
        MultipleAssociationRequest request,
        IUserService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTeams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAdAccounts(
        MultipleAssociationRequest request,
        IUserService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAdAccounts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAdAccounts(
        MultipleAssociationRequest request,
        IUserService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAdAccounts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static User mapRequestToUser( UserRequest request ) {
        var model = new User
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Role = request.Role,
        };
        return model;
    }

}
