using Microsoft.EntityFrameworkCore;

using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<Agency> Agencys => Set<Agency>();
public DbSet<Team> Teams => Set<Team>();
public DbSet<User> Users => Set<User>();
public DbSet<Advertiser> Advertisers => Set<Advertiser>();
public DbSet<BillingProfile> BillingProfiles => Set<BillingProfile>();
public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
public DbSet<AdAccount> AdAccounts => Set<AdAccount>();
public DbSet<DSP> DSPs => Set<DSP>();
public DbSet<Campaign> Campaigns => Set<Campaign>();
public DbSet<KPI> KPIs => Set<KPI>();
public DbSet<AudienceSegment> AudienceSegments => Set<AudienceSegment>();
public DbSet<DataProvider> DataProviders => Set<DataProvider>();
public DbSet<LineItem> LineItems => Set<LineItem>();
public DbSet<TargetingProfile> TargetingProfiles => Set<TargetingProfile>();
public DbSet<DeviceCriterion> DeviceCriterions => Set<DeviceCriterion>();
public DbSet<BrandSafetyPolicy> BrandSafetyPolicys => Set<BrandSafetyPolicy>();
public DbSet<ContentCategory> ContentCategorys => Set<ContentCategory>();
public DbSet<Publisher> Publishers => Set<Publisher>();
public DbSet<InventorySource> InventorySources => Set<InventorySource>();
public DbSet<AdSlot> AdSlots => Set<AdSlot>();
public DbSet<Deal> Deals => Set<Deal>();
public DbSet<Placement> Placements => Set<Placement>();
public DbSet<CreativeAsset> CreativeAssets => Set<CreativeAsset>();
public DbSet<CreativeFile> CreativeFiles => Set<CreativeFile>();
public DbSet<CreativeVariation> CreativeVariations => Set<CreativeVariation>();
public DbSet<CreativeApproval> CreativeApprovals => Set<CreativeApproval>();
public DbSet<TrackingPixel> TrackingPixels => Set<TrackingPixel>();
public DbSet<ConversionEvent> ConversionEvents => Set<ConversionEvent>();
public DbSet<PerformanceMetric> PerformanceMetrics => Set<PerformanceMetric>();
public DbSet<Report> Reports => Set<Report>();
public DbSet<InsertionOrder> InsertionOrders => Set<InsertionOrder>();
public DbSet<RateCard> RateCards => Set<RateCard>();
public DbSet<Rate> Rates => Set<Rate>();
public DbSet<Experiment> Experiments => Set<Experiment>();
public DbSet<ExperimentVariant> ExperimentVariants => Set<ExperimentVariant>();
public DbSet<GeoRegion> GeoRegions => Set<GeoRegion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Agency has one or more Advertisers of type Advertiser
        modelBuilder.Entity<Advertiser>()
            .HasOne<Agency>()
            .WithMany(parent => parent.Advertisers)
            .HasForeignKey("AdvertisersId");

        // Agency has one or more Teams of type Team
        modelBuilder.Entity<Team>()
            .HasOne<Agency>()
            .WithMany(parent => parent.Teams)
            .HasForeignKey("TeamsId");

        // Agency has one or more Users of type User
        modelBuilder.Entity<User>()
            .HasOne<Agency>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("UsersId");

        // Agency has one or more InsertionOrders of type InsertionOrder
        modelBuilder.Entity<InsertionOrder>()
            .HasOne<Agency>()
            .WithMany(parent => parent.InsertionOrders)
            .HasForeignKey("InsertionOrdersId");

        // Team has one Agency of type Agency
        modelBuilder.Entity<Team>()
            .HasOne(x => x.Agency)
            .WithMany()
            .HasForeignKey("AgencyId");


        // Team has one or more Users of type User
        modelBuilder.Entity<User>()
            .HasOne<Team>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("UsersId");

        // Team has one or more AdAccounts of type AdAccount
        modelBuilder.Entity<AdAccount>()
            .HasOne<Team>()
            .WithMany(parent => parent.AdAccounts)
            .HasForeignKey("AdAccountsId");

        // User has one Agency of type Agency
        modelBuilder.Entity<User>()
            .HasOne(x => x.Agency)
            .WithMany()
            .HasForeignKey("AgencyId");


        // User has one or more Teams of type Team
        modelBuilder.Entity<Team>()
            .HasOne<User>()
            .WithMany(parent => parent.Teams)
            .HasForeignKey("TeamsId");

        // User has one or more AdAccounts of type AdAccount
        modelBuilder.Entity<AdAccount>()
            .HasOne<User>()
            .WithMany(parent => parent.AdAccounts)
            .HasForeignKey("AdAccountsId");

        // Advertiser has one Agency of type Agency
        modelBuilder.Entity<Advertiser>()
            .HasOne(x => x.Agency)
            .WithMany()
            .HasForeignKey("AgencyId");


        // Advertiser has one or more AdAccounts of type AdAccount
        modelBuilder.Entity<AdAccount>()
            .HasOne<Advertiser>()
            .WithMany(parent => parent.AdAccounts)
            .HasForeignKey("AdAccountsId");

        // Advertiser has one or more BillingProfiles of type BillingProfile
        modelBuilder.Entity<BillingProfile>()
            .HasOne<Advertiser>()
            .WithMany(parent => parent.BillingProfiles)
            .HasForeignKey("BillingProfilesId");

        // Advertiser has one or more Campaigns of type Campaign
        modelBuilder.Entity<Campaign>()
            .HasOne<Advertiser>()
            .WithMany(parent => parent.Campaigns)
            .HasForeignKey("CampaignsId");

        // Advertiser has one or more TrackingPixels of type TrackingPixel
        modelBuilder.Entity<TrackingPixel>()
            .HasOne<Advertiser>()
            .WithMany(parent => parent.TrackingPixels)
            .HasForeignKey("TrackingPixelsId");

        // BillingProfile has one Advertiser of type Advertiser
        modelBuilder.Entity<BillingProfile>()
            .HasOne(x => x.Advertiser)
            .WithMany()
            .HasForeignKey("AdvertiserId");


        // BillingProfile has one or more PaymentMethods of type PaymentMethod
        modelBuilder.Entity<PaymentMethod>()
            .HasOne<BillingProfile>()
            .WithMany(parent => parent.PaymentMethods)
            .HasForeignKey("PaymentMethodsId");

        // BillingProfile has one or more AdAccounts of type AdAccount
        modelBuilder.Entity<AdAccount>()
            .HasOne<BillingProfile>()
            .WithMany(parent => parent.AdAccounts)
            .HasForeignKey("AdAccountsId");

        // PaymentMethod has one BillingProfile of type BillingProfile
        modelBuilder.Entity<PaymentMethod>()
            .HasOne(x => x.BillingProfile)
            .WithMany()
            .HasForeignKey("BillingProfileId");


        // AdAccount has one Advertiser of type Advertiser
        modelBuilder.Entity<AdAccount>()
            .HasOne(x => x.Advertiser)
            .WithMany()
            .HasForeignKey("AdvertiserId");

        // AdAccount has one BillingProfile of type BillingProfile
        modelBuilder.Entity<AdAccount>()
            .HasOne(x => x.BillingProfile)
            .WithMany()
            .HasForeignKey("BillingProfileId");

        // AdAccount has one Dsp of type DSP
        modelBuilder.Entity<AdAccount>()
            .HasOne(x => x.Dsp)
            .WithMany()
            .HasForeignKey("DspId");


        // AdAccount has one or more Users of type User
        modelBuilder.Entity<User>()
            .HasOne<AdAccount>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("UsersId");

        // AdAccount has one or more Campaigns of type Campaign
        modelBuilder.Entity<Campaign>()
            .HasOne<AdAccount>()
            .WithMany(parent => parent.Campaigns)
            .HasForeignKey("CampaignsId");

        // AdAccount has one or more PerformanceMetrics of type PerformanceMetric
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne<AdAccount>()
            .WithMany(parent => parent.PerformanceMetrics)
            .HasForeignKey("PerformanceMetricsId");


        // DSP has one or more AdAccounts of type AdAccount
        modelBuilder.Entity<AdAccount>()
            .HasOne<DSP>()
            .WithMany(parent => parent.AdAccounts)
            .HasForeignKey("AdAccountsId");

        // Campaign has one AdAccount of type AdAccount
        modelBuilder.Entity<Campaign>()
            .HasOne(x => x.AdAccount)
            .WithMany()
            .HasForeignKey("AdAccountId");

        // Campaign has one InsertionOrder of type InsertionOrder
        modelBuilder.Entity<Campaign>()
            .HasOne(x => x.InsertionOrder)
            .WithMany()
            .HasForeignKey("InsertionOrderId");


        // Campaign has one or more LineItems of type LineItem
        modelBuilder.Entity<LineItem>()
            .HasOne<Campaign>()
            .WithMany(parent => parent.LineItems)
            .HasForeignKey("LineItemsId");

        // Campaign has one or more Kpis of type KPI
        modelBuilder.Entity<KPI>()
            .HasOne<Campaign>()
            .WithMany(parent => parent.Kpis)
            .HasForeignKey("KpisId");

        // Campaign has one or more TrackingPixels of type TrackingPixel
        modelBuilder.Entity<TrackingPixel>()
            .HasOne<Campaign>()
            .WithMany(parent => parent.TrackingPixels)
            .HasForeignKey("TrackingPixelsId");

        // Campaign has one or more Audiences of type AudienceSegment
        modelBuilder.Entity<AudienceSegment>()
            .HasOne<Campaign>()
            .WithMany(parent => parent.Audiences)
            .HasForeignKey("AudiencesId");

        // Campaign has one or more Reports of type Report
        modelBuilder.Entity<Report>()
            .HasOne<Campaign>()
            .WithMany(parent => parent.Reports)
            .HasForeignKey("ReportsId");

        // KPI has one Campaign of type Campaign
        modelBuilder.Entity<KPI>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("CampaignId");


        // AudienceSegment has one Provider of type DataProvider
        modelBuilder.Entity<AudienceSegment>()
            .HasOne(x => x.Provider)
            .WithMany()
            .HasForeignKey("ProviderId");


        // AudienceSegment has one or more Campaigns of type Campaign
        modelBuilder.Entity<Campaign>()
            .HasOne<AudienceSegment>()
            .WithMany(parent => parent.Campaigns)
            .HasForeignKey("CampaignsId");


        // DataProvider has one or more AudienceSegments of type AudienceSegment
        modelBuilder.Entity<AudienceSegment>()
            .HasOne<DataProvider>()
            .WithMany(parent => parent.AudienceSegments)
            .HasForeignKey("AudienceSegmentsId");

        // LineItem has one Campaign of type Campaign
        modelBuilder.Entity<LineItem>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("CampaignId");

        // LineItem has one TargetingProfile of type TargetingProfile
        modelBuilder.Entity<LineItem>()
            .HasOne(x => x.TargetingProfile)
            .WithMany()
            .HasForeignKey("TargetingProfileId");

        // LineItem has one Deal of type Deal
        modelBuilder.Entity<LineItem>()
            .HasOne(x => x.Deal)
            .WithMany()
            .HasForeignKey("DealId");


        // LineItem has one or more Placements of type Placement
        modelBuilder.Entity<Placement>()
            .HasOne<LineItem>()
            .WithMany(parent => parent.Placements)
            .HasForeignKey("PlacementsId");

        // LineItem has one or more Creatives of type CreativeAsset
        modelBuilder.Entity<CreativeAsset>()
            .HasOne<LineItem>()
            .WithMany(parent => parent.Creatives)
            .HasForeignKey("CreativesId");

        // LineItem has one or more PerformanceMetrics of type PerformanceMetric
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne<LineItem>()
            .WithMany(parent => parent.PerformanceMetrics)
            .HasForeignKey("PerformanceMetricsId");

        // TargetingProfile has one BrandSafetyPolicy of type BrandSafetyPolicy
        modelBuilder.Entity<TargetingProfile>()
            .HasOne(x => x.BrandSafetyPolicy)
            .WithMany()
            .HasForeignKey("BrandSafetyPolicyId");


        // TargetingProfile has one or more AudienceSegments of type AudienceSegment
        modelBuilder.Entity<AudienceSegment>()
            .HasOne<TargetingProfile>()
            .WithMany(parent => parent.AudienceSegments)
            .HasForeignKey("AudienceSegmentsId");

        // TargetingProfile has one or more GeoRegions of type GeoRegion
        modelBuilder.Entity<GeoRegion>()
            .HasOne<TargetingProfile>()
            .WithMany(parent => parent.GeoRegions)
            .HasForeignKey("GeoRegionsId");

        // TargetingProfile has one or more ContentCategories of type ContentCategory
        modelBuilder.Entity<ContentCategory>()
            .HasOne<TargetingProfile>()
            .WithMany(parent => parent.ContentCategories)
            .HasForeignKey("ContentCategoriesId");

        // TargetingProfile has one or more DeviceCriteria of type DeviceCriterion
        modelBuilder.Entity<DeviceCriterion>()
            .HasOne<TargetingProfile>()
            .WithMany(parent => parent.DeviceCriteria)
            .HasForeignKey("DeviceCriteriaId");

        // DeviceCriterion has one TargetingProfile of type TargetingProfile
        modelBuilder.Entity<DeviceCriterion>()
            .HasOne(x => x.TargetingProfile)
            .WithMany()
            .HasForeignKey("TargetingProfileId");



        // BrandSafetyPolicy has one or more TargetingProfiles of type TargetingProfile
        modelBuilder.Entity<TargetingProfile>()
            .HasOne<BrandSafetyPolicy>()
            .WithMany(parent => parent.TargetingProfiles)
            .HasForeignKey("TargetingProfilesId");



        // Publisher has one or more InventorySources of type InventorySource
        modelBuilder.Entity<InventorySource>()
            .HasOne<Publisher>()
            .WithMany(parent => parent.InventorySources)
            .HasForeignKey("InventorySourcesId");

        // Publisher has one or more Deals of type Deal
        modelBuilder.Entity<Deal>()
            .HasOne<Publisher>()
            .WithMany(parent => parent.Deals)
            .HasForeignKey("DealsId");

        // Publisher has one or more CreativeApprovals of type CreativeApproval
        modelBuilder.Entity<CreativeApproval>()
            .HasOne<Publisher>()
            .WithMany(parent => parent.CreativeApprovals)
            .HasForeignKey("CreativeApprovalsId");

        // Publisher has one or more InsertionOrders of type InsertionOrder
        modelBuilder.Entity<InsertionOrder>()
            .HasOne<Publisher>()
            .WithMany(parent => parent.InsertionOrders)
            .HasForeignKey("InsertionOrdersId");

        // Publisher has one or more RateCards of type RateCard
        modelBuilder.Entity<RateCard>()
            .HasOne<Publisher>()
            .WithMany(parent => parent.RateCards)
            .HasForeignKey("RateCardsId");

        // InventorySource has one Publisher of type Publisher
        modelBuilder.Entity<InventorySource>()
            .HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey("PublisherId");


        // InventorySource has one or more AdSlots of type AdSlot
        modelBuilder.Entity<AdSlot>()
            .HasOne<InventorySource>()
            .WithMany(parent => parent.AdSlots)
            .HasForeignKey("AdSlotsId");

        // InventorySource has one or more Deals of type Deal
        modelBuilder.Entity<Deal>()
            .HasOne<InventorySource>()
            .WithMany(parent => parent.Deals)
            .HasForeignKey("DealsId");

        // AdSlot has one InventorySource of type InventorySource
        modelBuilder.Entity<AdSlot>()
            .HasOne(x => x.InventorySource)
            .WithMany()
            .HasForeignKey("InventorySourceId");


        // AdSlot has one or more Placements of type Placement
        modelBuilder.Entity<Placement>()
            .HasOne<AdSlot>()
            .WithMany(parent => parent.Placements)
            .HasForeignKey("PlacementsId");

        // AdSlot has one or more Rates of type Rate
        modelBuilder.Entity<Rate>()
            .HasOne<AdSlot>()
            .WithMany(parent => parent.Rates)
            .HasForeignKey("RatesId");

        // Deal has one Publisher of type Publisher
        modelBuilder.Entity<Deal>()
            .HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey("PublisherId");


        // Deal has one or more InventorySources of type InventorySource
        modelBuilder.Entity<InventorySource>()
            .HasOne<Deal>()
            .WithMany(parent => parent.InventorySources)
            .HasForeignKey("InventorySourcesId");

        // Deal has one or more Placements of type Placement
        modelBuilder.Entity<Placement>()
            .HasOne<Deal>()
            .WithMany(parent => parent.Placements)
            .HasForeignKey("PlacementsId");

        // Placement has one LineItem of type LineItem
        modelBuilder.Entity<Placement>()
            .HasOne(x => x.LineItem)
            .WithMany()
            .HasForeignKey("LineItemId");

        // Placement has one AdSlot of type AdSlot
        modelBuilder.Entity<Placement>()
            .HasOne(x => x.AdSlot)
            .WithMany()
            .HasForeignKey("AdSlotId");

        // Placement has one Deal of type Deal
        modelBuilder.Entity<Placement>()
            .HasOne(x => x.Deal)
            .WithMany()
            .HasForeignKey("DealId");



        // CreativeAsset has one or more Files of type CreativeFile
        modelBuilder.Entity<CreativeFile>()
            .HasOne<CreativeAsset>()
            .WithMany(parent => parent.Files)
            .HasForeignKey("FilesId");

        // CreativeAsset has one or more Approvals of type CreativeApproval
        modelBuilder.Entity<CreativeApproval>()
            .HasOne<CreativeAsset>()
            .WithMany(parent => parent.Approvals)
            .HasForeignKey("ApprovalsId");

        // CreativeAsset has one or more Variations of type CreativeVariation
        modelBuilder.Entity<CreativeVariation>()
            .HasOne<CreativeAsset>()
            .WithMany(parent => parent.Variations)
            .HasForeignKey("VariationsId");

        // CreativeAsset has one or more LineItems of type LineItem
        modelBuilder.Entity<LineItem>()
            .HasOne<CreativeAsset>()
            .WithMany(parent => parent.LineItems)
            .HasForeignKey("LineItemsId");

        // CreativeFile has one CreativeAsset of type CreativeAsset
        modelBuilder.Entity<CreativeFile>()
            .HasOne(x => x.CreativeAsset)
            .WithMany()
            .HasForeignKey("CreativeAssetId");


        // CreativeVariation has one CreativeAsset of type CreativeAsset
        modelBuilder.Entity<CreativeVariation>()
            .HasOne(x => x.CreativeAsset)
            .WithMany()
            .HasForeignKey("CreativeAssetId");


        // CreativeApproval has one CreativeAsset of type CreativeAsset
        modelBuilder.Entity<CreativeApproval>()
            .HasOne(x => x.CreativeAsset)
            .WithMany()
            .HasForeignKey("CreativeAssetId");

        // CreativeApproval has one Publisher of type Publisher
        modelBuilder.Entity<CreativeApproval>()
            .HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey("PublisherId");


        // TrackingPixel has one Campaign of type Campaign
        modelBuilder.Entity<TrackingPixel>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("CampaignId");

        // TrackingPixel has one Advertiser of type Advertiser
        modelBuilder.Entity<TrackingPixel>()
            .HasOne(x => x.Advertiser)
            .WithMany()
            .HasForeignKey("AdvertiserId");


        // TrackingPixel has one or more ConversionEvents of type ConversionEvent
        modelBuilder.Entity<ConversionEvent>()
            .HasOne<TrackingPixel>()
            .WithMany(parent => parent.ConversionEvents)
            .HasForeignKey("ConversionEventsId");

        // ConversionEvent has one Campaign of type Campaign
        modelBuilder.Entity<ConversionEvent>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("CampaignId");

        // ConversionEvent has one LineItem of type LineItem
        modelBuilder.Entity<ConversionEvent>()
            .HasOne(x => x.LineItem)
            .WithMany()
            .HasForeignKey("LineItemId");

        // ConversionEvent has one TrackingPixel of type TrackingPixel
        modelBuilder.Entity<ConversionEvent>()
            .HasOne(x => x.TrackingPixel)
            .WithMany()
            .HasForeignKey("TrackingPixelId");


        // PerformanceMetric has one AdAccount of type AdAccount
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne(x => x.AdAccount)
            .WithMany()
            .HasForeignKey("AdAccountId");

        // PerformanceMetric has one Campaign of type Campaign
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("CampaignId");

        // PerformanceMetric has one LineItem of type LineItem
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne(x => x.LineItem)
            .WithMany()
            .HasForeignKey("LineItemId");

        // PerformanceMetric has one Placement of type Placement
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne(x => x.Placement)
            .WithMany()
            .HasForeignKey("PlacementId");

        // PerformanceMetric has one CreativeAsset of type CreativeAsset
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne(x => x.CreativeAsset)
            .WithMany()
            .HasForeignKey("CreativeAssetId");


        // Report has one AdAccount of type AdAccount
        modelBuilder.Entity<Report>()
            .HasOne(x => x.AdAccount)
            .WithMany()
            .HasForeignKey("AdAccountId");

        // Report has one Campaign of type Campaign
        modelBuilder.Entity<Report>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("CampaignId");

        // Report has one LineItem of type LineItem
        modelBuilder.Entity<Report>()
            .HasOne(x => x.LineItem)
            .WithMany()
            .HasForeignKey("LineItemId");


        // InsertionOrder has one Advertiser of type Advertiser
        modelBuilder.Entity<InsertionOrder>()
            .HasOne(x => x.Advertiser)
            .WithMany()
            .HasForeignKey("AdvertiserId");

        // InsertionOrder has one Agency of type Agency
        modelBuilder.Entity<InsertionOrder>()
            .HasOne(x => x.Agency)
            .WithMany()
            .HasForeignKey("AgencyId");

        // InsertionOrder has one Publisher of type Publisher
        modelBuilder.Entity<InsertionOrder>()
            .HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey("PublisherId");


        // InsertionOrder has one or more Campaigns of type Campaign
        modelBuilder.Entity<Campaign>()
            .HasOne<InsertionOrder>()
            .WithMany(parent => parent.Campaigns)
            .HasForeignKey("CampaignsId");

        // RateCard has one Publisher of type Publisher
        modelBuilder.Entity<RateCard>()
            .HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey("PublisherId");


        // RateCard has one or more Rates of type Rate
        modelBuilder.Entity<Rate>()
            .HasOne<RateCard>()
            .WithMany(parent => parent.Rates)
            .HasForeignKey("RatesId");

        // Rate has one RateCard of type RateCard
        modelBuilder.Entity<Rate>()
            .HasOne(x => x.RateCard)
            .WithMany()
            .HasForeignKey("RateCardId");

        // Rate has one AdSlot of type AdSlot
        modelBuilder.Entity<Rate>()
            .HasOne(x => x.AdSlot)
            .WithMany()
            .HasForeignKey("AdSlotId");


        // Experiment has one Campaign of type Campaign
        modelBuilder.Entity<Experiment>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("CampaignId");


        // Experiment has one or more Variants of type ExperimentVariant
        modelBuilder.Entity<ExperimentVariant>()
            .HasOne<Experiment>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("VariantsId");

        // ExperimentVariant has one Experiment of type Experiment
        modelBuilder.Entity<ExperimentVariant>()
            .HasOne(x => x.Experiment)
            .WithMany()
            .HasForeignKey("ExperimentId");

        // ExperimentVariant has one CreativeVariation of type CreativeVariation
        modelBuilder.Entity<ExperimentVariant>()
            .HasOne(x => x.CreativeVariation)
            .WithMany()
            .HasForeignKey("CreativeVariationId");

        // ExperimentVariant has one LineItem of type LineItem
        modelBuilder.Entity<ExperimentVariant>()
            .HasOne(x => x.LineItem)
            .WithMany()
            .HasForeignKey("LineItemId");


        // GeoRegion has one Parent of type GeoRegion
        modelBuilder.Entity<GeoRegion>()
            .HasOne(x => x.Parent)
            .WithMany()
            .HasForeignKey("ParentId");


        // GeoRegion has one or more Children of type GeoRegion
        modelBuilder.Entity<GeoRegion>()
            .HasOne<GeoRegion>()
            .WithMany(parent => parent.Children)
            .HasForeignKey("ChildrenId");

    }
}
