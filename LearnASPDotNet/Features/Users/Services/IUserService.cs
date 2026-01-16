using LearnASPDotNet.Features.Users.Dtos;

namespace LearnASPDotNet.Features.Users.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(CreateUserDto createUserDto);
        Task<UserResponseDto> FindOneUserByIdAsync(string id);
        Task<List<UserResponseDto>> FindAllUsersAsync();
        Task<UserResponseDto> UpdateUserAsync(string userId, UpdateUserDto updateUserDto);
        Task<UserResponseDto> DeleteUserAsync(string userId);
        Task<bool> CheckExistEmailOrUsername(string emailOrUsername);

    }
}
