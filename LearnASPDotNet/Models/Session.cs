using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Session.Models
{
    public class Session
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("userId")]
        public string UserId { get; set; } = null!;

        [BsonElement("refreshToken")]
        public string RefreshToken { get; set; } = null!;

        [BsonElement("expiresAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)] // Ensure UTC storage in MongoDB and deletion based on UTC
        public DateTime ExpiresAt { get; set; }

    }
}
