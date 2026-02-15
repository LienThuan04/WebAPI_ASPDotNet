using LearnASPDotNet.Features.Users.Models;

namespace LearnASPDotNet.Features.Auths.Repositories
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
        Task CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(string email);
    }
}
