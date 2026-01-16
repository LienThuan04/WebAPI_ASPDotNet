using LearnASPDotNet.Features.Users.Dtos;
using LearnASPDotNet.Features.Users.Models;
using MongoDB.Driver;

namespace LearnASPDotNet.Features.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;
        // Constructor to initialize the MongoDB collection
        public UserRepository(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<User>("users");
        }

        // Method to get a user by their ID
        public async Task<User?> GetUserByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> CheckExistEmailOrUsername(string emailOrUsername)
        {
            var filter = Builders<User>.Filter.Or(
                Builders<User>.Filter.Eq(u => u.Email, emailOrUsername),
                Builders<User>.Filter.Eq(u => u.Username, emailOrUsername)
            );
            var user = await _usersCollection.Find(filter).FirstOrDefaultAsync();
            return user != null;
        }
        public async Task CreateUserAsync(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _usersCollection.Find(_ => true)
                .Project(user => new User
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<User?> UpdateUserAsync(string userId, UpdateUserDto updateUserDto)
        {
            // Tạo filter để tìm user theo userId
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            // Tạo update definition để cập nhật các trường
            var update = Builders<User>.Update
                .Set(u => u.UpdatedAt, DateTime.UtcNow);
            foreach (var field in typeof(UpdateUserDto).GetProperties())
            {
                //if (updateUserDto.Address != null)
                //{
                //    update = update.Set(u => u.Address, updateUserDto.Address);
                //}
                // Cập nhật các trường khác tương tự
                var value = field.GetValue(updateUserDto);
                if (value != null)
                {
                    update = update.Set(field.Name, value);
                }
            }
            //if(await this.CheckExistEmailOrUsername(updateUserDto.Email ?? "") && updateUserDto.Email != null)
            //{
            //    throw new Exception("The Email is Exist");
            //}
            var updatedUser = await _usersCollection.FindOneAndUpdateAsync(
                filter,
                update,
                new FindOneAndUpdateOptions<User, User?>
                {
                    ReturnDocument = ReturnDocument.After // Trả về bản sau khi update
                }
            );

            return updatedUser; // Có thể null nếu không tìm thấy user
        }


        public async Task<User?> DeleteUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var deletedUser = await _usersCollection.FindOneAndDeleteAsync(filter);
            return deletedUser;
        }
    }
}
