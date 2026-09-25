using advertisingonaspdotnet.Api;
using advertisingonaspdotnet.Persistence;
using advertisingonaspdotnet.Service;
using advertisingonaspdotnet.Telemetry;
using OpenTelemetry.Metrics;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

var dbEngineEnvironment = builder.Configuration["DB_TYPE"];

string dbEngine;
string dbUserName;
string dbPassword;
string dbName;
string dbHost;
string dbPort;

if (string.IsNullOrWhiteSpace(dbEngineEnvironment))
{
    // Generation-time configuration
    dbEngine = "mysql";
    dbUserName = "postgres";
    dbPassword = "no_password";
    dbName = "testDb";
    dbHost = "localhost";
    dbPort = "5432";
}
else
{
    // Runtime environment configuration
    dbEngine = dbEngineEnvironment;
    dbUserName = builder.Configuration["DB_USER_NAME"]
        ?? throw new InvalidOperationException("DB_USER_NAME is required when DB_ENGINE is provided.");

    dbPassword = builder.Configuration["DB_PASSWORD"]
        ?? throw new InvalidOperationException("DB_PASSWORD is required when DB_ENGINE is provided.");

    dbName = builder.Configuration["DB_NAME"]
        ?? throw new InvalidOperationException("DB_NAME is required when DB_ENGINE is provided.");

    dbHost = builder.Configuration["DB_HOST"]
        ?? throw new InvalidOperationException("DB_HOST is required when DB_ENGINE is provided.");

    dbPort = builder.Configuration["DB_PORT"]
        ?? throw new InvalidOperationException("DB_PORT is required when DB_ENGINE is provided.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString =
        $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUserName};Password={dbPassword}";

    if (dbEngine.Equals("mysql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("mariadb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(connectionString);
    }
    else if (dbEngine.Equals("sqlserver", StringComparison.OrdinalIgnoreCase) ||
             dbEngine.Equals("azuresql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else if (dbEngine.Equals("oracle", StringComparison.OrdinalIgnoreCase))
    {
        options.UseOracle(connectionString);
    }
    else if (dbEngine.Equals("mongodb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMongoDB(connectionString);
    }
    else if (dbEngine.Equals("inmemory", StringComparison.OrdinalIgnoreCase))
    {
        options.UseInMemoryDatabase("advertisingonaspdotnet");
    }
    else if (dbEngine.Equals("cosmosdb", StringComparison.OrdinalIgnoreCase))
    {
        var cosmosEndpoint = builder.Configuration["DB_ENDPOINT"]
            ?? throw new InvalidOperationException(
                "DB_ENDPOINT is required for Cosmos DB.");

        var cosmosKey = builder.Configuration["DB_KEY"]
            ?? throw new InvalidOperationException(
                "DB_KEY is required for Cosmos DB.");

        options.UseCosmos(
            cosmosEndpoint,
            cosmosKey,
            dbName);
    }
    else if (dbEngine.Equals("postgres", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        throw new InvalidOperationException(
            "Unsupported or missing database engine configuration.");
    }
    // etc.
});

    builder.Services.AddScoped<IAgencyRepository, AgencyRepository>();
    builder.Services.AddScoped<ITeamRepository, TeamRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IAdvertiserRepository, AdvertiserRepository>();
    builder.Services.AddScoped<IBillingProfileRepository, BillingProfileRepository>();
    builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
    builder.Services.AddScoped<IAdAccountRepository, AdAccountRepository>();
    builder.Services.AddScoped<IDSPRepository, DSPRepository>();
    builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
    builder.Services.AddScoped<IKPIRepository, KPIRepository>();
    builder.Services.AddScoped<IAudienceSegmentRepository, AudienceSegmentRepository>();
    builder.Services.AddScoped<IDataProviderRepository, DataProviderRepository>();
    builder.Services.AddScoped<ILineItemRepository, LineItemRepository>();
    builder.Services.AddScoped<ITargetingProfileRepository, TargetingProfileRepository>();
    builder.Services.AddScoped<IDeviceCriterionRepository, DeviceCriterionRepository>();
    builder.Services.AddScoped<IBrandSafetyPolicyRepository, BrandSafetyPolicyRepository>();
    builder.Services.AddScoped<IContentCategoryRepository, ContentCategoryRepository>();
    builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
    builder.Services.AddScoped<IInventorySourceRepository, InventorySourceRepository>();
    builder.Services.AddScoped<IAdSlotRepository, AdSlotRepository>();
    builder.Services.AddScoped<IDealRepository, DealRepository>();
    builder.Services.AddScoped<IPlacementRepository, PlacementRepository>();
    builder.Services.AddScoped<ICreativeAssetRepository, CreativeAssetRepository>();
    builder.Services.AddScoped<ICreativeFileRepository, CreativeFileRepository>();
    builder.Services.AddScoped<ICreativeVariationRepository, CreativeVariationRepository>();
    builder.Services.AddScoped<ICreativeApprovalRepository, CreativeApprovalRepository>();
    builder.Services.AddScoped<ITrackingPixelRepository, TrackingPixelRepository>();
    builder.Services.AddScoped<IConversionEventRepository, ConversionEventRepository>();
    builder.Services.AddScoped<IPerformanceMetricRepository, PerformanceMetricRepository>();
    builder.Services.AddScoped<IReportRepository, ReportRepository>();
    builder.Services.AddScoped<IInsertionOrderRepository, InsertionOrderRepository>();
    builder.Services.AddScoped<IRateCardRepository, RateCardRepository>();
    builder.Services.AddScoped<IRateRepository, RateRepository>();
    builder.Services.AddScoped<IExperimentRepository, ExperimentRepository>();
    builder.Services.AddScoped<IExperimentVariantRepository, ExperimentVariantRepository>();
    builder.Services.AddScoped<IGeoRegionRepository, GeoRegionRepository>();

    builder.Services.AddScoped<IAgencyService, AgencyService>();
    builder.Services.AddScoped<ITeamService, TeamService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IAdvertiserService, AdvertiserService>();
    builder.Services.AddScoped<IBillingProfileService, BillingProfileService>();
    builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
    builder.Services.AddScoped<IAdAccountService, AdAccountService>();
    builder.Services.AddScoped<IDSPService, DSPService>();
    builder.Services.AddScoped<ICampaignService, CampaignService>();
    builder.Services.AddScoped<IKPIService, KPIService>();
    builder.Services.AddScoped<IAudienceSegmentService, AudienceSegmentService>();
    builder.Services.AddScoped<IDataProviderService, DataProviderService>();
    builder.Services.AddScoped<ILineItemService, LineItemService>();
    builder.Services.AddScoped<ITargetingProfileService, TargetingProfileService>();
    builder.Services.AddScoped<IDeviceCriterionService, DeviceCriterionService>();
    builder.Services.AddScoped<IBrandSafetyPolicyService, BrandSafetyPolicyService>();
    builder.Services.AddScoped<IContentCategoryService, ContentCategoryService>();
    builder.Services.AddScoped<IPublisherService, PublisherService>();
    builder.Services.AddScoped<IInventorySourceService, InventorySourceService>();
    builder.Services.AddScoped<IAdSlotService, AdSlotService>();
    builder.Services.AddScoped<IDealService, DealService>();
    builder.Services.AddScoped<IPlacementService, PlacementService>();
    builder.Services.AddScoped<ICreativeAssetService, CreativeAssetService>();
    builder.Services.AddScoped<ICreativeFileService, CreativeFileService>();
    builder.Services.AddScoped<ICreativeVariationService, CreativeVariationService>();
    builder.Services.AddScoped<ICreativeApprovalService, CreativeApprovalService>();
    builder.Services.AddScoped<ITrackingPixelService, TrackingPixelService>();
    builder.Services.AddScoped<IConversionEventService, ConversionEventService>();
    builder.Services.AddScoped<IPerformanceMetricService, PerformanceMetricService>();
    builder.Services.AddScoped<IReportService, ReportService>();
    builder.Services.AddScoped<IInsertionOrderService, InsertionOrderService>();
    builder.Services.AddScoped<IRateCardService, RateCardService>();
    builder.Services.AddScoped<IRateService, RateService>();
    builder.Services.AddScoped<IExperimentService, ExperimentService>();
    builder.Services.AddScoped<IExperimentVariantService, ExperimentVariantService>();
    builder.Services.AddScoped<IGeoRegionService, GeoRegionService>();

// apply the service resolver
builder.Services.AddScoped<IServiceResolver, ServiceResolver>();

builder.Services.AddSingleton<ApplicationTelemetry>();

builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("Harbormaster.Application");

        metrics.AddPrometheusExporter();
    });

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();


// Health endpoint
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


    app.MapAgencyEndpoints();
    app.MapTeamEndpoints();
    app.MapUserEndpoints();
    app.MapAdvertiserEndpoints();
    app.MapBillingProfileEndpoints();
    app.MapPaymentMethodEndpoints();
    app.MapAdAccountEndpoints();
    app.MapDSPEndpoints();
    app.MapCampaignEndpoints();
    app.MapKPIEndpoints();
    app.MapAudienceSegmentEndpoints();
    app.MapDataProviderEndpoints();
    app.MapLineItemEndpoints();
    app.MapTargetingProfileEndpoints();
    app.MapDeviceCriterionEndpoints();
    app.MapBrandSafetyPolicyEndpoints();
    app.MapContentCategoryEndpoints();
    app.MapPublisherEndpoints();
    app.MapInventorySourceEndpoints();
    app.MapAdSlotEndpoints();
    app.MapDealEndpoints();
    app.MapPlacementEndpoints();
    app.MapCreativeAssetEndpoints();
    app.MapCreativeFileEndpoints();
    app.MapCreativeVariationEndpoints();
    app.MapCreativeApprovalEndpoints();
    app.MapTrackingPixelEndpoints();
    app.MapConversionEventEndpoints();
    app.MapPerformanceMetricEndpoints();
    app.MapReportEndpoints();
    app.MapInsertionOrderEndpoints();
    app.MapRateCardEndpoints();
    app.MapRateEndpoints();
    app.MapExperimentEndpoints();
    app.MapExperimentVariantEndpoints();
    app.MapGeoRegionEndpoints();

app.Run();

public partial class Program { }

