using LearnASPDotNet.Features.Users.Dtos;
using LearnASPDotNet.Features.Users.Models;

namespace LearnASPDotNet.Features.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
        Task<List<User>?> GetAllUsersAsync();
        Task<User?> DeleteUserAsync(string id);
        Task<User?> UpdateUserAsync(string userId, UpdateUserDto updateUserDto);
        Task<bool> CheckExistEmailOrUsername(string emailOrUsername);
    }
}
