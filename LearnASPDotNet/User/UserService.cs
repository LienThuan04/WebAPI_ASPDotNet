using MongoDB.Driver;
using Microsoft.Extensions.Options;
using LearnASPDotNet.Settings;
using LearnASPDotNet.Users.Models;

namespace LearnASPDotNet.Users.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection; // MongoDB collection for users
        public UserService(IOptions<MongoDbSettings> mongoDbSettings, IMongoClient mongoClient, IMongoDatabase database) // Inject MongoDB settings and client
        {
            //var database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName); // Get the database
            _usersCollection = database.GetCollection<User>("users"); // Get the "users" collection
        }
        public async Task<User?> GetUserByUsernameAsync(string username) // Retrieve a user by username
        {
            return await _usersCollection.Find(user => user.Username == username).FirstOrDefaultAsync();// Find the user in the collection
        }

        public async Task<User?> GetUserByIdAsync(string id) // Retrieve a user by ID
        {
            return await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync(); // Find the user in the collection
        }
        public async Task CreateUserAsync(User user) // Create a new user
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task<bool> CheckExistUserName(string username) // Check if a username exists
        {
            var user = await _usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAllUserAsync() // Retrieve all users
        {
            return await _usersCollection.Find(_ => true).ToListAsync(); // Get all users in the collection with _ is a filter that matches all documents
        }
    }
}
