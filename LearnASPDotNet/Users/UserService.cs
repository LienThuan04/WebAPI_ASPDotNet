using MongoDB.Driver;
using Microsoft.Extensions.Options;
using LearnASPDotNet.Settings;
using LearnASPDotNet.Users.Models;
using LearnASPDotNet.Users.Dtos;

namespace LearnASPDotNet.Users.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection; // MongoDB collection for users  
        public UserService(IOptions<MongoDbSettings> mongoDbSettings, IMongoClient mongoClient, IMongoDatabase database) // Inject MongoDB settings and client  
        {
            _usersCollection = database.GetCollection<User>("users"); // Get the "users" collection  
        }

        public async Task<User?> GetUserByUsernameAsync(string username) // Retrieve a user by username use for login
        {
            return await _usersCollection.Find(user => user.Username == username).FirstOrDefaultAsync(); // Find the user in the collection  
        }

        public async Task<UserDto?> GetUserByIdAsync(string id) // Retrieve a user by ID  
        {
            var User = await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync(); // Find the user in the collection
            if (User == null)
            {
                return null;
            }
            return new UserDto
            {
                Id = User.Id,
                Username = User.Username,
                Email = User.Email,
                Phone = User.Phone,
                Address = User.Address,
                CreatedAt = User.CreatedAt,
                UpdatedAt = User.UpdatedAt
            };
        }

        public async Task<bool> CheckExistEmailOrUsername(string emailOrUsername) // Check if an email exists  
        {
            var user = await _usersCollection.Find(u => u.Email == emailOrUsername || u.Username == emailOrUsername).FirstOrDefaultAsync();
            return user != null;
        }
        public async Task CreateUserAsync(User user) // Create a new user  
        {
            if (this.CheckExistEmailOrUsername(user.Email).Result)
            {
                throw new Exception("Email already exists.");
            }
            else if (this.CheckExistEmailOrUsername(user.Username).Result)
            {
                throw new Exception("Username already exists.");
            }
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task<List<User>> GetAllUserAsync() // Retrieve all users  
        {
            return await _usersCollection.Find(_ => true)
                .Project(user => new User // Project to exclude PasswordHash field
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                })
                .ToListAsync(); // Get all users in the collection with _ is a filter that matches all documents  
        }

        public async Task<UserDto?> UpdateUserAsync(string id, UpdateUserDto updatedUserDto) // Update an existing user  
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, id); // Filter to find the user by ID  

            var update = Builders<User>.Update // Create an update definition
                .Set(user => user.UpdatedAt, DateTime.UtcNow); // Update fields and set UpdatedAt
            // if the field is not null or empty, then set the field
            if (!string.IsNullOrEmpty(updatedUserDto.Email))
            {
                update = update.Set(user => user.Email, updatedUserDto.Email);
            }
            if (!string.IsNullOrEmpty(updatedUserDto.Phone))
            {
                update = update.Set(user => user.Phone, updatedUserDto.Phone);
            }
            if (!string.IsNullOrEmpty(updatedUserDto.Address))
            {
                update = update.Set(user => user.Address, updatedUserDto.Address);
            }
            // Apply the update and return the updated user
            var UserUpdate = await _usersCollection.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<User>
            {
                ReturnDocument = ReturnDocument.After // Return the updated document
            }); // Apply the update
            return new UserDto
            {
                Id = UserUpdate.Id,
                Username = UserUpdate.Username,
                Email = UserUpdate.Email,
                Phone = UserUpdate.Phone,
                Address = UserUpdate.Address,
                CreatedAt = UserUpdate.CreatedAt,
                UpdatedAt = UserUpdate.UpdatedAt ?? DateTime.UtcNow
            };
        }

        public async Task<User?> DeleteUserAsync(string id) // Delete a user by ID  
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, id); // Filter to find the user by ID
            var deleteUser = await _usersCollection.FindOneAndDeleteAsync(filter); // Delete the user from the collection
            return deleteUser;
        }
    }
}
