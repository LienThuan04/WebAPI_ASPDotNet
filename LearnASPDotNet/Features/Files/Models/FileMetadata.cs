using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LearnASPDotNet.Features.Files.Models
{
    public class FileMetadata
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = null!;

        [BsonElement("fileName")]
        public string FileName { get; set; } = null!;

        [BsonElement("filePath")]
        public string FilePath { get; set; } = null!;

        [BsonElement("fileUrl")]
        public string FileUrl { get; set; } = null!;

        [BsonElement("fileType")]
        public string FileType { get; set; } = null!;  // avatar, document, image

        [BsonElement("contentType")]
        public string ContentType { get; set; } = null!;

        [BsonElement("fileSize")]
        public long FileSize { get; set; }

        [BsonElement("uploadedAt")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; } = false;
    }
    
}