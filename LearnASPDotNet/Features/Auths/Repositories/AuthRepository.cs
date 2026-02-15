using LearnASPDotNet.Features.Users.Models;
using MongoDB.Driver;

namespace LearnASPDotNet.Features.Auths.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IMongoCollection<User> _usersCollection;

        public AuthRepository(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<User>("users");
        }

        public async Task<User?> GetUserByUsernameOrEmailAsync(string usernameOrEmail) // find user by username or email
        {
            var filter = Builders<User>.Filter.Or(
                    Builders<User>.Filter.Eq(u => u.Username, usernameOrEmail),
                    Builders<User>.Filter.Eq(u => u.Email, usernameOrEmail)
                );
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
