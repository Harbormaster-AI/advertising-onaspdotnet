
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class AdAccountEndpoints
{
    public static IEndpointRouteBuilder MapAdAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/adAccount").WithTags("AdAccounts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAdvertiser", AssignAdvertiser);
        group.MapPut("/unassignAdvertiser", UnassignAdvertiser);
        group.MapPut("/assignBillingProfile", AssignBillingProfile);
        group.MapPut("/unassignBillingProfile", UnassignBillingProfile);
        group.MapPut("/assignDsp", AssignDsp);
        group.MapPut("/unassignDsp", UnassignDsp);

    group.MapPut("/addToUsers", AddToUsers);
    group.MapPut("/removeFromUsers", RemoveFromUsers);

    group.MapPut("/addToCampaigns", AddToCampaigns);
    group.MapPut("/removeFromCampaigns", RemoveFromCampaigns);

    group.MapPut("/addToPerformanceMetrics", AddToPerformanceMetrics);
    group.MapPut("/removeFromPerformanceMetrics", RemoveFromPerformanceMetrics);


        return app;
    }

    private static async Task<IResult> Create(
        AdAccountRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAdAccount( request );

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
        AdAccountRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAdAccount( request );

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
        IAdAccountService service,
        CancellationToken cancellationToken) {

        var adAccount = await service.Get(identifier, cancellationToken);
        return adAccount is null ? Results.NotFound() : Results.Ok( adAccount );
    }


    private static async Task<IResult> GetAll(
        IAdAccountService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AdAccountResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAdAccountService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdvertiser(
        AssociationRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdvertiser(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdvertiser(
    AssociationRequest request,
    IAdAccountService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdvertiser(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBillingProfile(
        AssociationRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBillingProfile(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBillingProfile(
    AssociationRequest request,
    IAdAccountService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBillingProfile(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDsp(
        AssociationRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDsp(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDsp(
    AssociationRequest request,
    IAdAccountService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDsp(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToUsers(
        MultipleAssociationRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToUsers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromUsers(
        MultipleAssociationRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromUsers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCampaigns(
        MultipleAssociationRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCampaigns(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCampaigns(
        MultipleAssociationRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCampaigns(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPerformanceMetrics(
        MultipleAssociationRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPerformanceMetrics(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPerformanceMetrics(
        MultipleAssociationRequest request,
        IAdAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPerformanceMetrics(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AdAccount mapRequestToAdAccount( AdAccountRequest request ) {
        var model = new AdAccount
        {
            Id = request.Id,
            Name = request.Name,
            AccountCode = request.AccountCode,
            DefaultCurrency = request.DefaultCurrency,
            DefaultTimezone = request.DefaultTimezone,
        };
        return model;
    }

}
