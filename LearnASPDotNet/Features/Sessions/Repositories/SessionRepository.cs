using MongoDB.Driver;
using LearnASPDotNet.Features.Sessions.Dtos;
using LearnASPDotNet.Features.Sessions.Models;

namespace LearnASPDotNet.Features.Sessions.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IMongoCollection<Session> _sessionsCollection;
        public SessionRepository(IMongoDatabase database)
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

        public async Task<Session?> UpSertSessionAsync(Session session)
        {
            var filter = Builders<Session>.Filter.Eq(s => s.UserId, session.UserId);
            var update = Builders<Session>.Update
                .Set(s => s.RefreshToken, session.RefreshToken)
                .Set(s => s.ExpiresAt, session.ExpiresAt);
            var options = new FindOneAndUpdateOptions<Session>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true // If no document matches the filter, insert a new document
            };
            var updatedSession = await _sessionsCollection.FindOneAndUpdateAsync(filter, update, options);
            return updatedSession;
        }

        public async Task<Session?> GetSessionWithRefreshTokenAndUserIdAsync(SessionRequestDto sessionRequestDto)
        {
            var filter = Builders<Session>.Filter.And(
                Builders<Session>.Filter.Eq(s => s.RefreshToken, sessionRequestDto.RefreshToken),
                Builders<Session>.Filter.Eq(s => s.UserId, sessionRequestDto.UserId)
            );
            return await _sessionsCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Session?> UpdateSessionAsync(SessionRequestDto sessionRequestDto, DateTime newExpiresAt)
        {
            var filter = Builders<Session>.Filter.Eq(s => s.UserId, sessionRequestDto.UserId);
            var update = Builders<Session>.Update
                .Set(s => s.RefreshToken, sessionRequestDto.RefreshToken)
                .Set(s => s.ExpiresAt, newExpiresAt);
            var options = new FindOneAndUpdateOptions<Session>
            {
                ReturnDocument = ReturnDocument.After
            };
            return await _sessionsCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<Session?> DeleteSessionAsync(SessionRequestDto sessionRequestDto)
        {
            var filter = Builders<Session>.Filter.And(
                Builders<Session>.Filter.Eq(s => s.RefreshToken, sessionRequestDto.RefreshToken),
                Builders<Session>.Filter.Eq(s => s.UserId, sessionRequestDto.UserId)
            );
            return await _sessionsCollection.FindOneAndDeleteAsync(filter);
        }
    }
}
