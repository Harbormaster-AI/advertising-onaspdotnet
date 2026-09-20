using advertisingonaspdotnet.Domain;

namespace advertisingonaspdotnet.Contracts;

public class IdentifierRequest
{
    public Guid Id { get; set; }
}

public class AssociationRequest
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
}

public class MultipleAssociationRequest
{
    public Guid ParentId { get; set; }
    public List<Guid> ChildIds { get; set; } = new();
}

public class AgencyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? HeadquartersCountry { get; set; } 
 public virtual string? Website { get; set; } 
}

public class AgencyResponse : AgencyRequest {
    public static AgencyResponse FromModel(Agency model) {
        return new AgencyResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            HeadquartersCountry = model.HeadquartersCountry,
            Website = model.Website,
        };
    }
}

public class TeamRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
}

public class TeamResponse : TeamRequest {
    public static TeamResponse FromModel(Team model) {
        return new TeamResponse {
            Id = model.Id,
            Name = model.Name,
        };
    }
}

public class UserRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual Email? Email { get; set; } 
 public virtual AccountRole? Role { get; set; } 
}

public class UserResponse : UserRequest {
    public static UserResponse FromModel(User model) {
        return new UserResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Role = model.Role,
        };
    }
}

public class AdvertiserRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? Industry { get; set; } 
 public virtual string? Website { get; set; } 
}

public class AdvertiserResponse : AdvertiserRequest {
    public static AdvertiserResponse FromModel(Advertiser model) {
        return new AdvertiserResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            Industry = model.Industry,
            Website = model.Website,
        };
    }
}

public class BillingProfileRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? BillingName { get; set; } 
 public virtual string? TaxId { get; set; } 
 public virtual Address? BillingAddress { get; set; } 
 public virtual PaymentTerms? PaymentTerms { get; set; } 
}

public class BillingProfileResponse : BillingProfileRequest {
    public static BillingProfileResponse FromModel(BillingProfile model) {
        return new BillingProfileResponse {
            Id = model.Id,
            BillingName = model.BillingName,
            TaxId = model.TaxId,
            BillingAddress = model.BillingAddress,
            PaymentTerms = model.PaymentTerms,
        };
    }
}

public class PaymentMethodRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Last4 { get; set; } 
 public virtual string? CardholderName { get; set; } 
 public virtual Address? BillingAddress { get; set; } 
 public virtual PaymentMethodType? MethodType { get; set; } 
}

public class PaymentMethodResponse : PaymentMethodRequest {
    public static PaymentMethodResponse FromModel(PaymentMethod model) {
        return new PaymentMethodResponse {
            Id = model.Id,
            Last4 = model.Last4,
            CardholderName = model.CardholderName,
            BillingAddress = model.BillingAddress,
            MethodType = model.MethodType,
        };
    }
}

public class AdAccountRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? AccountCode { get; set; } 
 public virtual string? DefaultCurrency { get; set; } 
 public virtual string? DefaultTimezone { get; set; } 
}

public class AdAccountResponse : AdAccountRequest {
    public static AdAccountResponse FromModel(AdAccount model) {
        return new AdAccountResponse {
            Id = model.Id,
            Name = model.Name,
            AccountCode = model.AccountCode,
            DefaultCurrency = model.DefaultCurrency,
            DefaultTimezone = model.DefaultTimezone,
        };
    }
}

public class DSPRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual string? Region { get; set; } 
}

public class DSPResponse : DSPRequest {
    public static DSPResponse FromModel(DSP model) {
        return new DSPResponse {
            Id = model.Id,
            Name = model.Name,
            Website = model.Website,
            Region = model.Region,
        };
    }
}

public class CampaignRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Money? TotalBudget { get; set; } 
 public virtual DateRange? Flight { get; set; } 
 public virtual ObjectiveType? Objective { get; set; } 
 public virtual CampaignStatus? Status { get; set; } 
}

public class CampaignResponse : CampaignRequest {
    public static CampaignResponse FromModel(Campaign model) {
        return new CampaignResponse {
            Id = model.Id,
            Name = model.Name,
            TotalBudget = model.TotalBudget,
            Flight = model.Flight,
            Objective = model.Objective,
            Status = model.Status,
        };
    }
}

public class KPIRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual decimal? TargetValue { get; set; } 
 public virtual MetricType? MetricType { get; set; } 
}

public class KPIResponse : KPIRequest {
    public static KPIResponse FromModel(KPI model) {
        return new KPIResponse {
            Id = model.Id,
            TargetValue = model.TargetValue,
            MetricType = model.MetricType,
        };
    }
}

public class AudienceSegmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual int? EstimatedReach { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual DataProviderType? ProviderType { get; set; } 
}

public class AudienceSegmentResponse : AudienceSegmentRequest {
    public static AudienceSegmentResponse FromModel(AudienceSegment model) {
        return new AudienceSegmentResponse {
            Id = model.Id,
            Name = model.Name,
            EstimatedReach = model.EstimatedReach,
            Description = model.Description,
            ProviderType = model.ProviderType,
        };
    }
}

public class DataProviderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual DataProviderType? ProviderType { get; set; } 
}

public class DataProviderResponse : DataProviderRequest {
    public static DataProviderResponse FromModel(DataProvider model) {
        return new DataProviderResponse {
            Id = model.Id,
            Name = model.Name,
            Website = model.Website,
            ProviderType = model.ProviderType,
        };
    }
}

public class LineItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Money? BidAmount { get; set; } 
 public virtual Money? DailyBudget { get; set; } 
 public virtual FrequencyCap? FrequencyCap { get; set; } 
 public virtual LineItemStatus? Status { get; set; } 
 public virtual PricingModel? PricingModel { get; set; } 
 public virtual BidStrategyType? BidStrategy { get; set; } 
 public virtual PacingType? Pacing { get; set; } 
}

public class LineItemResponse : LineItemRequest {
    public static LineItemResponse FromModel(LineItem model) {
        return new LineItemResponse {
            Id = model.Id,
            Name = model.Name,
            BidAmount = model.BidAmount,
            DailyBudget = model.DailyBudget,
            FrequencyCap = model.FrequencyCap,
            Status = model.Status,
            PricingModel = model.PricingModel,
            BidStrategy = model.BidStrategy,
            Pacing = model.Pacing,
        };
    }
}

public class TargetingProfileRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
}

public class TargetingProfileResponse : TargetingProfileRequest {
    public static TargetingProfileResponse FromModel(TargetingProfile model) {
        return new TargetingProfileResponse {
            Id = model.Id,
            Name = model.Name,
        };
    }
}

public class DeviceCriterionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DeviceType? DeviceType { get; set; } 
 public virtual PlatformType? PlatformType { get; set; } 
 public virtual TargetingOperator? Operator_ { get; set; } 
}

public class DeviceCriterionResponse : DeviceCriterionRequest {
    public static DeviceCriterionResponse FromModel(DeviceCriterion model) {
        return new DeviceCriterionResponse {
            Id = model.Id,
            DeviceType = model.DeviceType,
            PlatformType = model.PlatformType,
            Operator_ = model.Operator_,
        };
    }
}

public class BrandSafetyPolicyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual BrandSafetyLevel? Level { get; set; } 
 public virtual ContentRating? ContentRatingThreshold { get; set; } 
}

public class BrandSafetyPolicyResponse : BrandSafetyPolicyRequest {
    public static BrandSafetyPolicyResponse FromModel(BrandSafetyPolicy model) {
        return new BrandSafetyPolicyResponse {
            Id = model.Id,
            Level = model.Level,
            ContentRatingThreshold = model.ContentRatingThreshold,
        };
    }
}

public class ContentCategoryRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Code { get; set; } 
 public virtual string? Name { get; set; } 
}

public class ContentCategoryResponse : ContentCategoryRequest {
    public static ContentCategoryResponse FromModel(ContentCategory model) {
        return new ContentCategoryResponse {
            Id = model.Id,
            Code = model.Code,
            Name = model.Name,
        };
    }
}

public class PublisherRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual PublisherType? PublisherType { get; set; } 
}

public class PublisherResponse : PublisherRequest {
    public static PublisherResponse FromModel(Publisher model) {
        return new PublisherResponse {
            Id = model.Id,
            Name = model.Name,
            Website = model.Website,
            PublisherType = model.PublisherType,
        };
    }
}

public class InventorySourceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Domain { get; set; } 
 public virtual ChannelType? Channel { get; set; } 
 public virtual AdFormat? PrimaryFormat { get; set; } 
}

public class InventorySourceResponse : InventorySourceRequest {
    public static InventorySourceResponse FromModel(InventorySource model) {
        return new InventorySourceResponse {
            Id = model.Id,
            Name = model.Name,
            Domain = model.Domain,
            Channel = model.Channel,
            PrimaryFormat = model.PrimaryFormat,
        };
    }
}

public class AdSlotRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? SlotCode { get; set; } 
 public virtual int? Width { get; set; } 
 public virtual int? Height { get; set; } 
 public virtual Money? FloorPrice { get; set; } 
 public virtual AdFormat? Format { get; set; } 
}

public class AdSlotResponse : AdSlotRequest {
    public static AdSlotResponse FromModel(AdSlot model) {
        return new AdSlotResponse {
            Id = model.Id,
            SlotCode = model.SlotCode,
            Width = model.Width,
            Height = model.Height,
            FloorPrice = model.FloorPrice,
            Format = model.Format,
        };
    }
}

public class DealRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Money? FloorPrice { get; set; } 
 public virtual DealType? DealType { get; set; } 
}

public class DealResponse : DealRequest {
    public static DealResponse FromModel(Deal model) {
        return new DealResponse {
            Id = model.Id,
            FloorPrice = model.FloorPrice,
            DealType = model.DealType,
        };
    }
}

public class PlacementRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual DateRange? Flight { get; set; } 
 public virtual int? GoalImpressions { get; set; } 
}

public class PlacementResponse : PlacementRequest {
    public static PlacementResponse FromModel(Placement model) {
        return new PlacementResponse {
            Id = model.Id,
            Name = model.Name,
            Flight = model.Flight,
            GoalImpressions = model.GoalImpressions,
        };
    }
}

public class CreativeAssetRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual URL? ClickUrl { get; set; } 
 public virtual URL? LandingPage { get; set; } 
 public virtual int? Width { get; set; } 
 public virtual int? Height { get; set; } 
 public virtual int? DurationSeconds { get; set; } 
 public virtual CreativeType? CreativeType { get; set; } 
 public virtual AdFormat? AdFormat { get; set; } 
}

public class CreativeAssetResponse : CreativeAssetRequest {
    public static CreativeAssetResponse FromModel(CreativeAsset model) {
        return new CreativeAssetResponse {
            Id = model.Id,
            Name = model.Name,
            ClickUrl = model.ClickUrl,
            LandingPage = model.LandingPage,
            Width = model.Width,
            Height = model.Height,
            DurationSeconds = model.DurationSeconds,
            CreativeType = model.CreativeType,
            AdFormat = model.AdFormat,
        };
    }
}

public class CreativeFileRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual URL? Uri { get; set; } 
 public virtual int? FileSizeKB { get; set; } 
 public virtual string? MimeType { get; set; } 
 public virtual string? Checksum { get; set; } 
}

public class CreativeFileResponse : CreativeFileRequest {
    public static CreativeFileResponse FromModel(CreativeFile model) {
        return new CreativeFileResponse {
            Id = model.Id,
            Uri = model.Uri,
            FileSizeKB = model.FileSizeKB,
            MimeType = model.MimeType,
            Checksum = model.Checksum,
        };
    }
}

public class CreativeVariationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Language { get; set; } 
 public virtual string? Headline { get; set; } 
 public virtual string? BodyText { get; set; } 
 public virtual string? CallToAction { get; set; } 
}

public class CreativeVariationResponse : CreativeVariationRequest {
    public static CreativeVariationResponse FromModel(CreativeVariation model) {
        return new CreativeVariationResponse {
            Id = model.Id,
            Name = model.Name,
            Language = model.Language,
            Headline = model.Headline,
            BodyText = model.BodyText,
            CallToAction = model.CallToAction,
        };
    }
}

public class CreativeApprovalRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Reviewer { get; set; } 
 public virtual DateOnly? ReviewedAt { get; set; } 
 public virtual CreativeApprovalStatus? Status { get; set; } 
}

public class CreativeApprovalResponse : CreativeApprovalRequest {
    public static CreativeApprovalResponse FromModel(CreativeApproval model) {
        return new CreativeApprovalResponse {
            Id = model.Id,
            Reviewer = model.Reviewer,
            ReviewedAt = model.ReviewedAt,
            Status = model.Status,
        };
    }
}

public class TrackingPixelRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual URL? Url { get; set; } 
 public virtual ConversionEventType? EventType { get; set; } 
 public virtual PixelType? PixelType { get; set; } 
}

public class TrackingPixelResponse : TrackingPixelRequest {
    public static TrackingPixelResponse FromModel(TrackingPixel model) {
        return new TrackingPixelResponse {
            Id = model.Id,
            Name = model.Name,
            Url = model.Url,
            EventType = model.EventType,
            PixelType = model.PixelType,
        };
    }
}

public class ConversionEventRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateTime? Timestamp { get; set; } 
 public virtual Money? Value { get; set; } 
 public virtual ConversionEventType? EventType { get; set; } 
 public virtual AttributionModel? AttributionModel { get; set; } 
}

public class ConversionEventResponse : ConversionEventRequest {
    public static ConversionEventResponse FromModel(ConversionEvent model) {
        return new ConversionEventResponse {
            Id = model.Id,
            Timestamp = model.Timestamp,
            Value = model.Value,
            EventType = model.EventType,
            AttributionModel = model.AttributionModel,
        };
    }
}

public class PerformanceMetricRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? Date { get; set; } 
 public virtual decimal? Value { get; set; } 
 public virtual MetricType? MetricType { get; set; } 
}

public class PerformanceMetricResponse : PerformanceMetricRequest {
    public static PerformanceMetricResponse FromModel(PerformanceMetric model) {
        return new PerformanceMetricResponse {
            Id = model.Id,
            Date = model.Date,
            Value = model.Value,
            MetricType = model.MetricType,
        };
    }
}

public class ReportRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ReportName { get; set; } 
 public virtual DateTime? GeneratedAt { get; set; } 
 public virtual URL? FileUrl { get; set; } 
 public virtual ReportType? ReportType { get; set; } 
}

public class ReportResponse : ReportRequest {
    public static ReportResponse FromModel(Report model) {
        return new ReportResponse {
            Id = model.Id,
            ReportName = model.ReportName,
            GeneratedAt = model.GeneratedAt,
            FileUrl = model.FileUrl,
            ReportType = model.ReportType,
        };
    }
}

public class InsertionOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? IoNumber { get; set; } 
 public virtual Money? AgreedBudget { get; set; } 
 public virtual DateRange? Flight { get; set; } 
 public virtual IOStatus? Status { get; set; } 
}

public class InsertionOrderResponse : InsertionOrderRequest {
    public static InsertionOrderResponse FromModel(InsertionOrder model) {
        return new InsertionOrderResponse {
            Id = model.Id,
            IoNumber = model.IoNumber,
            AgreedBudget = model.AgreedBudget,
            Flight = model.Flight,
            Status = model.Status,
        };
    }
}

public class RateCardRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual string? Currency { get; set; } 
}

public class RateCardResponse : RateCardRequest {
    public static RateCardResponse FromModel(RateCard model) {
        return new RateCardResponse {
            Id = model.Id,
            Name = model.Name,
            EffectiveDate = model.EffectiveDate,
            Currency = model.Currency,
        };
    }
}

public class RateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Money? UnitPrice { get; set; } 
 public virtual AdFormat? AdFormat { get; set; } 
 public virtual PricingModel? PricingModel { get; set; } 
}

public class RateResponse : RateRequest {
    public static RateResponse FromModel(Rate model) {
        return new RateResponse {
            Id = model.Id,
            UnitPrice = model.UnitPrice,
            AdFormat = model.AdFormat,
            PricingModel = model.PricingModel,
        };
    }
}

public class ExperimentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Hypothesis { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual ExperimentStatus? Status { get; set; } 
}

public class ExperimentResponse : ExperimentRequest {
    public static ExperimentResponse FromModel(Experiment model) {
        return new ExperimentResponse {
            Id = model.Id,
            Name = model.Name,
            Hypothesis = model.Hypothesis,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Status = model.Status,
        };
    }
}

public class ExperimentVariantRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Percentage? Allocation { get; set; } 
}

public class ExperimentVariantResponse : ExperimentVariantRequest {
    public static ExperimentVariantResponse FromModel(ExperimentVariant model) {
        return new ExperimentVariantResponse {
            Id = model.Id,
            Name = model.Name,
            Allocation = model.Allocation,
        };
    }
}

public class GeoRegionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Code { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual GeoRegionType? RegionType { get; set; } 
}

public class GeoRegionResponse : GeoRegionRequest {
    public static GeoRegionResponse FromModel(GeoRegion model) {
        return new GeoRegionResponse {
            Id = model.Id,
            Code = model.Code,
            Name = model.Name,
            RegionType = model.RegionType,
        };
    }
}

