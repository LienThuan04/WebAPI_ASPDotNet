using MongoDB.Driver;
using LearnASPDotNet.Sessions.Models;

namespace LearnASPDotNet.Sessions.Services
{
    public class SessionService
    {
        private readonly IMongoCollection<Session> _sessionsCollection;

        public SessionService(IMongoDatabase database) //IMongoDatabase được setup ở Program.cs  
        {
            _sessionsCollection = database.GetCollection<Session>("Sessions");

            var indexKeys = Builders<Session>.IndexKeys.Ascending(session => session.ExpiresAt);
            var indexOptions = new CreateIndexOptions
            {
                ExpireAfter = TimeSpan.Zero // Xóa ngay khi tới ExpiresAt
            };
            var indexModel = new CreateIndexModel<Session>(indexKeys, indexOptions);
            _sessionsCollection.Indexes.CreateOne(indexModel);
        }

        //todo: CRUD session  
        public async Task<Session> UpSertSessionAsync(string refreshToken, string userId)
        {
            var refreshExpiryConfig = Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRE_DAYS");
            //var refreshExpiryConfig = 1.ToString(); // test expire one minute
            if (string.IsNullOrEmpty(refreshExpiryConfig))
            {
                throw new Exception("JWT_REFRESH_EXPIRE_DAYS is not configured");
            }
            var session = new Session
            {
                UserId = userId,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    int.Parse(refreshExpiryConfig)
                ) // Set expiration date for 7 days  
            };

            var filter = Builders<Session>.Filter.Eq(s => s.UserId, userId);
            var update = Builders<Session>.Update
                .Set(s => s.RefreshToken, session.RefreshToken)
                .Set(s => s.ExpiresAt, session.ExpiresAt);

            var options = new FindOneAndUpdateOptions<Session>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true // If no document matches the filter, insert a new document
            };

            var updatedSession = await _sessionsCollection.FindOneAndUpdateAsync(filter, update, options);
            if (updatedSession == null)
            {
                throw new Exception("Create session failed");
            }
            return updatedSession;
        }

        public async Task<Session?> GetSessionWithRefreshTokenAndUserId(string refreshToken, string userId)
        {
            return await _sessionsCollection
                .Find(session => session.RefreshToken == refreshToken && session.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<Session?> UpdateSession(string userId, string newRefreshToken)
        {
            var update = Builders<Session>.Update
                .Set(s => s.RefreshToken, newRefreshToken)
                .Set(s => s.ExpiresAt, DateTime.UtcNow.AddDays(
                    int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRE_DAYS")!)
                )); // Update expiration date  

            var result = await _sessionsCollection.FindOneAndUpdateAsync(
                s => s.UserId == userId, // Filter to find the session by userId  
                update, // Update definition  
                new FindOneAndUpdateOptions<Session>
                {
                    ReturnDocument = ReturnDocument.After
                }
            );
            return result;
        }

        public async Task<bool> DeleteSessionByRefreshToken(string refreshToken, string userId)
        {
            var filter = Builders<Session>.Filter.And(
                Builders<Session>.Filter.Eq(session => session.RefreshToken, refreshToken),
                Builders<Session>.Filter.Eq(session => session.UserId, userId)
            );

            var result = await _sessionsCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0; // Return true if a session was deleted  
        }
    }
}

