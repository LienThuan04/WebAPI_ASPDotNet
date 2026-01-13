using LearnASPDotNet.Features.Users.Models;

namespace LearnASPDotNet.Features.Auths
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(string email);
    }
}
