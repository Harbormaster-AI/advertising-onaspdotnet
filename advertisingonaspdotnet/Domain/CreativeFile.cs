
using advertisingonaspdotnet.Contracts;

namespace advertisingonaspdotnet.Domain;

public class CreativeFile
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CreativefileId { get; set; } 
 public virtual URL? Uri { get; set; } 
 public virtual int? FileSizeKB { get; set; } 
 public virtual string? MimeType { get; set; } 
 public virtual string? Checksum { get; set; } 
public virtual CreativeAsset? CreativeAsset { get; set; } 

    public static CreativeFile FromRequest(CreativeFileRequest request) {
        return new CreativeFile {
            Id = request.Id,
            Uri = request.Uri,
            FileSizeKB = request.FileSizeKB,
            MimeType = request.MimeType,
            Checksum = request.Checksum,
        };
    }
}
