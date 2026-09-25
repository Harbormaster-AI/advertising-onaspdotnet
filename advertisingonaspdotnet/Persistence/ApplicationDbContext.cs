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
            .HasForeignKey("Agency_Id");

        // Agency has one or more Teams of type Team
        modelBuilder.Entity<Team>()
            .HasOne<Agency>()
            .WithMany(parent => parent.Teams)
            .HasForeignKey("Agency_Id");

        // Agency has one or more Users of type User
        modelBuilder.Entity<User>()
            .HasOne<Agency>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("Agency_Id");

        // Agency has one or more InsertionOrders of type InsertionOrder
        modelBuilder.Entity<InsertionOrder>()
            .HasOne<Agency>()
            .WithMany(parent => parent.InsertionOrders)
            .HasForeignKey("Agency_Id");

        // Team has one Agency of type Agency
        modelBuilder.Entity<Team>()
            .HasOne(x => x.Agency)
            .WithMany()
            .HasForeignKey("Agency_Id");


        // Team has one or more Users of type User
        modelBuilder.Entity<User>()
            .HasOne<Team>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("Team_Id");

        // Team has one or more AdAccounts of type AdAccount
        modelBuilder.Entity<AdAccount>()
            .HasOne<Team>()
            .WithMany(parent => parent.AdAccounts)
            .HasForeignKey("Team_Id");

        // User has one Agency of type Agency
        modelBuilder.Entity<User>()
            .HasOne(x => x.Agency)
            .WithMany()
            .HasForeignKey("Agency_Id");


        // User has one or more Teams of type Team
        modelBuilder.Entity<Team>()
            .HasOne<User>()
            .WithMany(parent => parent.Teams)
            .HasForeignKey("User_Id");

        // User has one or more AdAccounts of type AdAccount
        modelBuilder.Entity<AdAccount>()
            .HasOne<User>()
            .WithMany(parent => parent.AdAccounts)
            .HasForeignKey("User_Id");

        // Advertiser has one Agency of type Agency
        modelBuilder.Entity<Advertiser>()
            .HasOne(x => x.Agency)
            .WithMany()
            .HasForeignKey("Agency_Id");


        // Advertiser has one or more AdAccounts of type AdAccount
        modelBuilder.Entity<AdAccount>()
            .HasOne<Advertiser>()
            .WithMany(parent => parent.AdAccounts)
            .HasForeignKey("Advertiser_Id");

        // Advertiser has one or more BillingProfiles of type BillingProfile
        modelBuilder.Entity<BillingProfile>()
            .HasOne<Advertiser>()
            .WithMany(parent => parent.BillingProfiles)
            .HasForeignKey("Advertiser_Id");

        // Advertiser has one or more Campaigns of type Campaign
        modelBuilder.Entity<Campaign>()
            .HasOne<Advertiser>()
            .WithMany(parent => parent.Campaigns)
            .HasForeignKey("Advertiser_Id");

        // Advertiser has one or more TrackingPixels of type TrackingPixel
        modelBuilder.Entity<TrackingPixel>()
            .HasOne<Advertiser>()
            .WithMany(parent => parent.TrackingPixels)
            .HasForeignKey("Advertiser_Id");

        // BillingProfile has one Advertiser of type Advertiser
        modelBuilder.Entity<BillingProfile>()
            .HasOne(x => x.Advertiser)
            .WithMany()
            .HasForeignKey("Advertiser_Id");


        // BillingProfile has one or more PaymentMethods of type PaymentMethod
        modelBuilder.Entity<PaymentMethod>()
            .HasOne<BillingProfile>()
            .WithMany(parent => parent.PaymentMethods)
            .HasForeignKey("BillingProfile_Id");

        // BillingProfile has one or more AdAccounts of type AdAccount
        modelBuilder.Entity<AdAccount>()
            .HasOne<BillingProfile>()
            .WithMany(parent => parent.AdAccounts)
            .HasForeignKey("BillingProfile_Id");

        // PaymentMethod has one BillingProfile of type BillingProfile
        modelBuilder.Entity<PaymentMethod>()
            .HasOne(x => x.BillingProfile)
            .WithMany()
            .HasForeignKey("BillingProfile_Id");


        // AdAccount has one Advertiser of type Advertiser
        modelBuilder.Entity<AdAccount>()
            .HasOne(x => x.Advertiser)
            .WithMany()
            .HasForeignKey("Advertiser_Id");

        // AdAccount has one BillingProfile of type BillingProfile
        modelBuilder.Entity<AdAccount>()
            .HasOne(x => x.BillingProfile)
            .WithMany()
            .HasForeignKey("BillingProfile_Id");

        // AdAccount has one Dsp of type DSP
        modelBuilder.Entity<AdAccount>()
            .HasOne(x => x.Dsp)
            .WithMany()
            .HasForeignKey("Dsp_Id");


        // AdAccount has one or more Users of type User
        modelBuilder.Entity<User>()
            .HasOne<AdAccount>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("AdAccount_Id");

        // AdAccount has one or more Campaigns of type Campaign
        modelBuilder.Entity<Campaign>()
            .HasOne<AdAccount>()
            .WithMany(parent => parent.Campaigns)
            .HasForeignKey("AdAccount_Id");

        // AdAccount has one or more PerformanceMetrics of type PerformanceMetric
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne<AdAccount>()
            .WithMany(parent => parent.PerformanceMetrics)
            .HasForeignKey("AdAccount_Id");


        // DSP has one or more AdAccounts of type AdAccount
        modelBuilder.Entity<AdAccount>()
            .HasOne<DSP>()
            .WithMany(parent => parent.AdAccounts)
            .HasForeignKey("DSP_Id");

        // Campaign has one AdAccount of type AdAccount
        modelBuilder.Entity<Campaign>()
            .HasOne(x => x.AdAccount)
            .WithMany()
            .HasForeignKey("AdAccount_Id");

        // Campaign has one InsertionOrder of type InsertionOrder
        modelBuilder.Entity<Campaign>()
            .HasOne(x => x.InsertionOrder)
            .WithMany()
            .HasForeignKey("InsertionOrder_Id");


        // Campaign has one or more LineItems of type LineItem
        modelBuilder.Entity<LineItem>()
            .HasOne<Campaign>()
            .WithMany(parent => parent.LineItems)
            .HasForeignKey("Campaign_Id");

        // Campaign has one or more Kpis of type KPI
        modelBuilder.Entity<KPI>()
            .HasOne<Campaign>()
            .WithMany(parent => parent.Kpis)
            .HasForeignKey("Campaign_Id");

        // Campaign has one or more TrackingPixels of type TrackingPixel
        modelBuilder.Entity<TrackingPixel>()
            .HasOne<Campaign>()
            .WithMany(parent => parent.TrackingPixels)
            .HasForeignKey("Campaign_Id");

        // Campaign has one or more Audiences of type AudienceSegment
        modelBuilder.Entity<AudienceSegment>()
            .HasOne<Campaign>()
            .WithMany(parent => parent.Audiences)
            .HasForeignKey("Campaign_Id");

        // Campaign has one or more Reports of type Report
        modelBuilder.Entity<Report>()
            .HasOne<Campaign>()
            .WithMany(parent => parent.Reports)
            .HasForeignKey("Campaign_Id");

        // KPI has one Campaign of type Campaign
        modelBuilder.Entity<KPI>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("Campaign_Id");


        // AudienceSegment has one Provider of type DataProvider
        modelBuilder.Entity<AudienceSegment>()
            .HasOne(x => x.Provider)
            .WithMany()
            .HasForeignKey("Provider_Id");


        // AudienceSegment has one or more Campaigns of type Campaign
        modelBuilder.Entity<Campaign>()
            .HasOne<AudienceSegment>()
            .WithMany(parent => parent.Campaigns)
            .HasForeignKey("AudienceSegment_Id");


        // DataProvider has one or more AudienceSegments of type AudienceSegment
        modelBuilder.Entity<AudienceSegment>()
            .HasOne<DataProvider>()
            .WithMany(parent => parent.AudienceSegments)
            .HasForeignKey("DataProvider_Id");

        // LineItem has one Campaign of type Campaign
        modelBuilder.Entity<LineItem>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("Campaign_Id");

        // LineItem has one TargetingProfile of type TargetingProfile
        modelBuilder.Entity<LineItem>()
            .HasOne(x => x.TargetingProfile)
            .WithMany()
            .HasForeignKey("TargetingProfile_Id");

        // LineItem has one Deal of type Deal
        modelBuilder.Entity<LineItem>()
            .HasOne(x => x.Deal)
            .WithMany()
            .HasForeignKey("Deal_Id");


        // LineItem has one or more Placements of type Placement
        modelBuilder.Entity<Placement>()
            .HasOne<LineItem>()
            .WithMany(parent => parent.Placements)
            .HasForeignKey("LineItem_Id");

        // LineItem has one or more Creatives of type CreativeAsset
        modelBuilder.Entity<CreativeAsset>()
            .HasOne<LineItem>()
            .WithMany(parent => parent.Creatives)
            .HasForeignKey("LineItem_Id");

        // LineItem has one or more PerformanceMetrics of type PerformanceMetric
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne<LineItem>()
            .WithMany(parent => parent.PerformanceMetrics)
            .HasForeignKey("LineItem_Id");

        // TargetingProfile has one BrandSafetyPolicy of type BrandSafetyPolicy
        modelBuilder.Entity<TargetingProfile>()
            .HasOne(x => x.BrandSafetyPolicy)
            .WithMany()
            .HasForeignKey("BrandSafetyPolicy_Id");


        // TargetingProfile has one or more AudienceSegments of type AudienceSegment
        modelBuilder.Entity<AudienceSegment>()
            .HasOne<TargetingProfile>()
            .WithMany(parent => parent.AudienceSegments)
            .HasForeignKey("TargetingProfile_Id");

        // TargetingProfile has one or more GeoRegions of type GeoRegion
        modelBuilder.Entity<GeoRegion>()
            .HasOne<TargetingProfile>()
            .WithMany(parent => parent.GeoRegions)
            .HasForeignKey("TargetingProfile_Id");

        // TargetingProfile has one or more ContentCategories of type ContentCategory
        modelBuilder.Entity<ContentCategory>()
            .HasOne<TargetingProfile>()
            .WithMany(parent => parent.ContentCategories)
            .HasForeignKey("TargetingProfile_Id");

        // TargetingProfile has one or more DeviceCriteria of type DeviceCriterion
        modelBuilder.Entity<DeviceCriterion>()
            .HasOne<TargetingProfile>()
            .WithMany(parent => parent.DeviceCriteria)
            .HasForeignKey("TargetingProfile_Id");

        // DeviceCriterion has one TargetingProfile of type TargetingProfile
        modelBuilder.Entity<DeviceCriterion>()
            .HasOne(x => x.TargetingProfile)
            .WithMany()
            .HasForeignKey("TargetingProfile_Id");



        // BrandSafetyPolicy has one or more TargetingProfiles of type TargetingProfile
        modelBuilder.Entity<TargetingProfile>()
            .HasOne<BrandSafetyPolicy>()
            .WithMany(parent => parent.TargetingProfiles)
            .HasForeignKey("BrandSafetyPolicy_Id");



        // Publisher has one or more InventorySources of type InventorySource
        modelBuilder.Entity<InventorySource>()
            .HasOne<Publisher>()
            .WithMany(parent => parent.InventorySources)
            .HasForeignKey("Publisher_Id");

        // Publisher has one or more Deals of type Deal
        modelBuilder.Entity<Deal>()
            .HasOne<Publisher>()
            .WithMany(parent => parent.Deals)
            .HasForeignKey("Publisher_Id");

        // Publisher has one or more CreativeApprovals of type CreativeApproval
        modelBuilder.Entity<CreativeApproval>()
            .HasOne<Publisher>()
            .WithMany(parent => parent.CreativeApprovals)
            .HasForeignKey("Publisher_Id");

        // Publisher has one or more InsertionOrders of type InsertionOrder
        modelBuilder.Entity<InsertionOrder>()
            .HasOne<Publisher>()
            .WithMany(parent => parent.InsertionOrders)
            .HasForeignKey("Publisher_Id");

        // Publisher has one or more RateCards of type RateCard
        modelBuilder.Entity<RateCard>()
            .HasOne<Publisher>()
            .WithMany(parent => parent.RateCards)
            .HasForeignKey("Publisher_Id");

        // InventorySource has one Publisher of type Publisher
        modelBuilder.Entity<InventorySource>()
            .HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey("Publisher_Id");


        // InventorySource has one or more AdSlots of type AdSlot
        modelBuilder.Entity<AdSlot>()
            .HasOne<InventorySource>()
            .WithMany(parent => parent.AdSlots)
            .HasForeignKey("InventorySource_Id");

        // InventorySource has one or more Deals of type Deal
        modelBuilder.Entity<Deal>()
            .HasOne<InventorySource>()
            .WithMany(parent => parent.Deals)
            .HasForeignKey("InventorySource_Id");

        // AdSlot has one InventorySource of type InventorySource
        modelBuilder.Entity<AdSlot>()
            .HasOne(x => x.InventorySource)
            .WithMany()
            .HasForeignKey("InventorySource_Id");


        // AdSlot has one or more Placements of type Placement
        modelBuilder.Entity<Placement>()
            .HasOne<AdSlot>()
            .WithMany(parent => parent.Placements)
            .HasForeignKey("AdSlot_Id");

        // AdSlot has one or more Rates of type Rate
        modelBuilder.Entity<Rate>()
            .HasOne<AdSlot>()
            .WithMany(parent => parent.Rates)
            .HasForeignKey("AdSlot_Id");

        // Deal has one Publisher of type Publisher
        modelBuilder.Entity<Deal>()
            .HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey("Publisher_Id");


        // Deal has one or more InventorySources of type InventorySource
        modelBuilder.Entity<InventorySource>()
            .HasOne<Deal>()
            .WithMany(parent => parent.InventorySources)
            .HasForeignKey("Deal_Id");

        // Deal has one or more Placements of type Placement
        modelBuilder.Entity<Placement>()
            .HasOne<Deal>()
            .WithMany(parent => parent.Placements)
            .HasForeignKey("Deal_Id");

        // Placement has one LineItem of type LineItem
        modelBuilder.Entity<Placement>()
            .HasOne(x => x.LineItem)
            .WithMany()
            .HasForeignKey("LineItem_Id");

        // Placement has one AdSlot of type AdSlot
        modelBuilder.Entity<Placement>()
            .HasOne(x => x.AdSlot)
            .WithMany()
            .HasForeignKey("AdSlot_Id");

        // Placement has one Deal of type Deal
        modelBuilder.Entity<Placement>()
            .HasOne(x => x.Deal)
            .WithMany()
            .HasForeignKey("Deal_Id");



        // CreativeAsset has one or more Files of type CreativeFile
        modelBuilder.Entity<CreativeFile>()
            .HasOne<CreativeAsset>()
            .WithMany(parent => parent.Files)
            .HasForeignKey("CreativeAsset_Id");

        // CreativeAsset has one or more Approvals of type CreativeApproval
        modelBuilder.Entity<CreativeApproval>()
            .HasOne<CreativeAsset>()
            .WithMany(parent => parent.Approvals)
            .HasForeignKey("CreativeAsset_Id");

        // CreativeAsset has one or more Variations of type CreativeVariation
        modelBuilder.Entity<CreativeVariation>()
            .HasOne<CreativeAsset>()
            .WithMany(parent => parent.Variations)
            .HasForeignKey("CreativeAsset_Id");

        // CreativeAsset has one or more LineItems of type LineItem
        modelBuilder.Entity<LineItem>()
            .HasOne<CreativeAsset>()
            .WithMany(parent => parent.LineItems)
            .HasForeignKey("CreativeAsset_Id");

        // CreativeFile has one CreativeAsset of type CreativeAsset
        modelBuilder.Entity<CreativeFile>()
            .HasOne(x => x.CreativeAsset)
            .WithMany()
            .HasForeignKey("CreativeAsset_Id");


        // CreativeVariation has one CreativeAsset of type CreativeAsset
        modelBuilder.Entity<CreativeVariation>()
            .HasOne(x => x.CreativeAsset)
            .WithMany()
            .HasForeignKey("CreativeAsset_Id");


        // CreativeApproval has one CreativeAsset of type CreativeAsset
        modelBuilder.Entity<CreativeApproval>()
            .HasOne(x => x.CreativeAsset)
            .WithMany()
            .HasForeignKey("CreativeAsset_Id");

        // CreativeApproval has one Publisher of type Publisher
        modelBuilder.Entity<CreativeApproval>()
            .HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey("Publisher_Id");


        // TrackingPixel has one Campaign of type Campaign
        modelBuilder.Entity<TrackingPixel>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("Campaign_Id");

        // TrackingPixel has one Advertiser of type Advertiser
        modelBuilder.Entity<TrackingPixel>()
            .HasOne(x => x.Advertiser)
            .WithMany()
            .HasForeignKey("Advertiser_Id");


        // TrackingPixel has one or more ConversionEvents of type ConversionEvent
        modelBuilder.Entity<ConversionEvent>()
            .HasOne<TrackingPixel>()
            .WithMany(parent => parent.ConversionEvents)
            .HasForeignKey("TrackingPixel_Id");

        // ConversionEvent has one Campaign of type Campaign
        modelBuilder.Entity<ConversionEvent>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("Campaign_Id");

        // ConversionEvent has one LineItem of type LineItem
        modelBuilder.Entity<ConversionEvent>()
            .HasOne(x => x.LineItem)
            .WithMany()
            .HasForeignKey("LineItem_Id");

        // ConversionEvent has one TrackingPixel of type TrackingPixel
        modelBuilder.Entity<ConversionEvent>()
            .HasOne(x => x.TrackingPixel)
            .WithMany()
            .HasForeignKey("TrackingPixel_Id");


        // PerformanceMetric has one AdAccount of type AdAccount
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne(x => x.AdAccount)
            .WithMany()
            .HasForeignKey("AdAccount_Id");

        // PerformanceMetric has one Campaign of type Campaign
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("Campaign_Id");

        // PerformanceMetric has one LineItem of type LineItem
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne(x => x.LineItem)
            .WithMany()
            .HasForeignKey("LineItem_Id");

        // PerformanceMetric has one Placement of type Placement
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne(x => x.Placement)
            .WithMany()
            .HasForeignKey("Placement_Id");

        // PerformanceMetric has one CreativeAsset of type CreativeAsset
        modelBuilder.Entity<PerformanceMetric>()
            .HasOne(x => x.CreativeAsset)
            .WithMany()
            .HasForeignKey("CreativeAsset_Id");


        // Report has one AdAccount of type AdAccount
        modelBuilder.Entity<Report>()
            .HasOne(x => x.AdAccount)
            .WithMany()
            .HasForeignKey("AdAccount_Id");

        // Report has one Campaign of type Campaign
        modelBuilder.Entity<Report>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("Campaign_Id");

        // Report has one LineItem of type LineItem
        modelBuilder.Entity<Report>()
            .HasOne(x => x.LineItem)
            .WithMany()
            .HasForeignKey("LineItem_Id");


        // InsertionOrder has one Advertiser of type Advertiser
        modelBuilder.Entity<InsertionOrder>()
            .HasOne(x => x.Advertiser)
            .WithMany()
            .HasForeignKey("Advertiser_Id");

        // InsertionOrder has one Agency of type Agency
        modelBuilder.Entity<InsertionOrder>()
            .HasOne(x => x.Agency)
            .WithMany()
            .HasForeignKey("Agency_Id");

        // InsertionOrder has one Publisher of type Publisher
        modelBuilder.Entity<InsertionOrder>()
            .HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey("Publisher_Id");


        // InsertionOrder has one or more Campaigns of type Campaign
        modelBuilder.Entity<Campaign>()
            .HasOne<InsertionOrder>()
            .WithMany(parent => parent.Campaigns)
            .HasForeignKey("InsertionOrder_Id");

        // RateCard has one Publisher of type Publisher
        modelBuilder.Entity<RateCard>()
            .HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey("Publisher_Id");


        // RateCard has one or more Rates of type Rate
        modelBuilder.Entity<Rate>()
            .HasOne<RateCard>()
            .WithMany(parent => parent.Rates)
            .HasForeignKey("RateCard_Id");

        // Rate has one RateCard of type RateCard
        modelBuilder.Entity<Rate>()
            .HasOne(x => x.RateCard)
            .WithMany()
            .HasForeignKey("RateCard_Id");

        // Rate has one AdSlot of type AdSlot
        modelBuilder.Entity<Rate>()
            .HasOne(x => x.AdSlot)
            .WithMany()
            .HasForeignKey("AdSlot_Id");


        // Experiment has one Campaign of type Campaign
        modelBuilder.Entity<Experiment>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("Campaign_Id");


        // Experiment has one or more Variants of type ExperimentVariant
        modelBuilder.Entity<ExperimentVariant>()
            .HasOne<Experiment>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("Experiment_Id");

        // ExperimentVariant has one Experiment of type Experiment
        modelBuilder.Entity<ExperimentVariant>()
            .HasOne(x => x.Experiment)
            .WithMany()
            .HasForeignKey("Experiment_Id");

        // ExperimentVariant has one CreativeVariation of type CreativeVariation
        modelBuilder.Entity<ExperimentVariant>()
            .HasOne(x => x.CreativeVariation)
            .WithMany()
            .HasForeignKey("CreativeVariation_Id");

        // ExperimentVariant has one LineItem of type LineItem
        modelBuilder.Entity<ExperimentVariant>()
            .HasOne(x => x.LineItem)
            .WithMany()
            .HasForeignKey("LineItem_Id");


        // GeoRegion has one Parent of type GeoRegion
        modelBuilder.Entity<GeoRegion>()
            .HasOne(x => x.Parent)
            .WithMany()
            .HasForeignKey("Parent_Id");


        // GeoRegion has one or more Children of type GeoRegion
        modelBuilder.Entity<GeoRegion>()
            .HasOne<GeoRegion>()
            .WithMany(parent => parent.Children)
            .HasForeignKey("GeoRegion_Id");

    }
}
