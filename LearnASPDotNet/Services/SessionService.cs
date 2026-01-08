using MongoDB.Driver;
//using Session.Models;

namespace Session.Services
{
    public class SessionService
    {
        private readonly IMongoCollection<Models.Session> _sessionsCollection;
        private readonly IConfiguration _configuration;

        public SessionService(IMongoDatabase database, IConfiguration configuration) //IMongoDatabase được setup ở Program.cs  
        {
            _sessionsCollection = database.GetCollection<Models.Session>("Sessions");
            _configuration = configuration;

            var indexKeys = Builders<Models.Session>.IndexKeys.Ascending(session => session.ExpiresAt);
            var indexOptions = new CreateIndexOptions
            {
                ExpireAfter = TimeSpan.Zero // Xóa ngay khi tới ExpiresAt
            };
            var indexModel = new CreateIndexModel<Models.Session>(indexKeys, indexOptions);
            _sessionsCollection.Indexes.CreateOne(indexModel);
        }

        //todo: CRUD session  
        public async Task<Models.Session> UpSertSessionAsync(string refreshToken, string userId)
        {
            var refreshExpiryConfig = this._configuration["Jwt:RefreshExpiryInDays"];
            //var refreshExpiryConfig = 1.ToString(); // test expire one minute
            if (string.IsNullOrEmpty(refreshExpiryConfig))
            {
                throw new Exception("JWT:RefreshExpiryInDays is not configured");
            }
            var session = new Models.Session
            {
                UserId = userId,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    int.Parse(refreshExpiryConfig)
                ) // Set expiration date for 7 days  
            };

            var filter = Builders<Models.Session>.Filter.Eq(s => s.UserId, userId);
            var update = Builders<Models.Session>.Update
                .Set(s => s.RefreshToken, session.RefreshToken)
                .Set(s => s.ExpiresAt, session.ExpiresAt);

            var options = new FindOneAndUpdateOptions<Models.Session>
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

        public async Task<Models.Session?> GetSessionWithRefreshTokenAndUserId(string refreshToken, string userId)
        {
            return await _sessionsCollection
                .Find(session => session.RefreshToken == refreshToken && session.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<Models.Session?> UpdateSession(string userId, string newRefreshToken)
        {
            var update = Builders<Models.Session>.Update
                .Set(s => s.RefreshToken, newRefreshToken)
                .Set(s => s.ExpiresAt, DateTime.UtcNow.AddDays(
                    int.Parse(this._configuration["Jwt:RefreshExpiryInDays"]!)
                )); // Update expiration date  

            var result = await _sessionsCollection.FindOneAndUpdateAsync(
                s => s.UserId == userId, // Filter to find the session by userId  
                update, // Update definition  
                new FindOneAndUpdateOptions<Models.Session>
                {
                    ReturnDocument = ReturnDocument.After
                }
            );
            return result;
        }

        public async Task<bool> DeleteSessionByRefreshToken(string refreshToken, string userId)
        {
            var filter = Builders<Models.Session>.Filter.And(
                Builders<Models.Session>.Filter.Eq(session => session.RefreshToken, refreshToken),
                Builders<Models.Session>.Filter.Eq(session => session.UserId, userId)
            );

            var result = await _sessionsCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0; // Return true if a session was deleted  
        }
    }
}

