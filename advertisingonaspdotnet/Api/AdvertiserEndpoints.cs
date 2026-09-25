
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Domain;
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Api;

public static class AdvertiserEndpoints
{
    public static IEndpointRouteBuilder MapAdvertiserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/advertiser").WithTags("Advertisers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAgency", AssignAgency);
        group.MapPut("/unassignAgency", UnassignAgency);

    group.MapPut("/addToAdAccounts", AddToAdAccounts);
    group.MapPut("/removeFromAdAccounts", RemoveFromAdAccounts);

    group.MapPut("/addToBillingProfiles", AddToBillingProfiles);
    group.MapPut("/removeFromBillingProfiles", RemoveFromBillingProfiles);

    group.MapPut("/addToCampaigns", AddToCampaigns);
    group.MapPut("/removeFromCampaigns", RemoveFromCampaigns);

    group.MapPut("/addToTrackingPixels", AddToTrackingPixels);
    group.MapPut("/removeFromTrackingPixels", RemoveFromTrackingPixels);


        return app;
    }

    private static async Task<IResult> Create(
        AdvertiserRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAdvertiser( request );

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
        AdvertiserRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAdvertiser( request );

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
        IAdvertiserService service,
        CancellationToken cancellationToken) {

        var advertiser = await service.Get(identifier, cancellationToken);
        return advertiser is null ? Results.NotFound() : Results.Ok( advertiser );
    }


    private static async Task<IResult> GetAll(
        IAdvertiserService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AdvertiserResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAdvertiserService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAgency(
        AssociationRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAgency(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAgency(
    AssociationRequest request,
    IAdvertiserService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAgency(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAdAccounts(
        MultipleAssociationRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAdAccounts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAdAccounts(
        MultipleAssociationRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAdAccounts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToBillingProfiles(
        MultipleAssociationRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBillingProfiles(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBillingProfiles(
        MultipleAssociationRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBillingProfiles(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCampaigns(
        MultipleAssociationRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCampaigns(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCampaigns(
        MultipleAssociationRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCampaigns(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTrackingPixels(
        MultipleAssociationRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTrackingPixels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTrackingPixels(
        MultipleAssociationRequest request,
        IAdvertiserService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTrackingPixels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Advertiser mapRequestToAdvertiser( AdvertiserRequest request ) {
        var model = new Advertiser
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            Industry = request.Industry,
            Website = request.Website,
        };
        return model;
    }

}
