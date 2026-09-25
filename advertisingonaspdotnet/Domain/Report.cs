
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class Report
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ReportId { get; set; } 
 public virtual string? ReportName { get; set; } 
 public virtual DateTime? GeneratedAt { get; set; } 
 public virtual URL? FileUrl { get; set; } 
public virtual AdAccount? AdAccount { get; set; } 
public virtual Campaign? Campaign { get; set; } 
public virtual LineItem? LineItem { get; set; } 
 public virtual ReportType? ReportType { get; set; } 

    public static Report FromRequest(ReportRequest request) {
        return new Report {
            Id = request.Id,
            ReportName = request.ReportName,
            GeneratedAt = request.GeneratedAt,
            FileUrl = request.FileUrl,
            ReportType = request.ReportType,
        };
    }
}
