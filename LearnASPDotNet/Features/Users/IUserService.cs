using LearnASPDotNet.Features.Users.Dtos;

namespace LearnASPDotNet.Features.Users
{
    public interface IUserService
    {
        Task CreateUserAsync(CreateUserDto createUserDto);
        Task<UserResponse> FindOneUserByIdAsync(string id);
        Task<List<UserResponse>> FindAllUsersAsync();
        Task<UserResponse> UpdateUserAsync(string userId, UpdateUserDto updateUserDto);
        Task<UserResponse> DeleteUserAsync(string userId);
        Task<bool> CheckExistEmailOrUsername(string emailOrUsername);

    }
}
