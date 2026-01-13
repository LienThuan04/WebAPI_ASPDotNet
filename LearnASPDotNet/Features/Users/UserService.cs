using LearnASPDotNet.Features.Users.Models;
using LearnASPDotNet.Features.Users.Dtos;

namespace LearnASPDotNet.Features.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) // Inject MongoDB settings and client  
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(CreateUserDto createUser)
        {
            try
            {
                var user = new User
                {
                    Username = createUser.Username,
                    Email = createUser.Email,
                    Phone = createUser.Phone ?? "",
                    Address = createUser.Address ?? "",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(!string.IsNullOrEmpty(createUser.Password) ? createUser.Password : Environment.GetEnvironmentVariable("DEFAULT_PASSWORD_ACCOUNT")!)
                };
                await _userRepository.CreateUserAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating user: " + ex.Message);
            }

        }

        public async Task<UserResponse?> FindOneUserByIdAsync(string id)
        {
            var User = await _userRepository.GetUserByIdAsync(id);
            return User == null ? null : new UserResponse
            {
                UserId = User.Id,
                Username = User.Username,
                Email = User.Email,
                Phone = User.Phone,
                Address = User.Address,
                CreatedAt = User.CreatedAt,
                UpdatedAt = User.UpdatedAt
            };
        }

        public async Task<List<UserResponse>> FindAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(user => new UserResponse
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            }).ToList();
        }

        public async Task<UserResponse?> UpdateUserAsync(string UserId, UpdateUserDto updateUser)
        {
            var user = await _userRepository.UpdateUserAsync(UserId, updateUser);
            if (user == null)
            {
                return null;
            }
            return new UserResponse
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<bool> CheckExistEmailOrUsername(string emailOrUsername)
        {
            return await _userRepository.CheckExistEmailOrUsername(emailOrUsername);
        }

        public async Task<UserResponse?> DeleteUserAsync(string id)
        {
            var user = await _userRepository.DeleteUserAsync(id);
            if (user == null)
            {
                return null;
            }
            return new UserResponse
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
