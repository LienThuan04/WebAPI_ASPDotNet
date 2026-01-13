using LearnASPDotNet.Features.Users.Models;
using MongoDB.Driver;

namespace LearnASPDotNet.Features.Auths
{
    public class AuthRepository : IAuthRepository
    {
        private  readonly IMongoCollection<User> _usersCollection;

        public AuthRepository(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<User>("users");
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task CreateUserAsync(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }

    }
}
