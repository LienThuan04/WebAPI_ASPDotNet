using LearnASPDotNet.Features.Auths.Dtos;
using LearnASPDotNet.Features.Users.Models;
using LearnASPDotNet.Features.Users.Dtos;
using LearnASPDotNet.Features.Auths.Repositories;

namespace LearnASPDotNet.Features.Auths.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly JwtService _jwtService;

        public AuthService(IAuthRepository authRepository, JwtService jwtService)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto.RegisterResponse> RegisterAsync(RegisterRequestDto request)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Phone = request.Phone ?? "",
                Address = request.Address ?? "",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            await _authRepository.CreateUserAsync(user);

            return new AuthResponseDto.RegisterResponse
            {
                Message = "User registered successfully."
            };
        }

        public async Task<AuthResponseDto.LoginResponse> LoginAsync(LoginRequestDto request)
        {
            var user = await _authRepository.GetUserByUsernameAsync(request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password.");
            }
            var payload = new JwtPayloadDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };
            var accessToken = _jwtService.GenerateToken(payload);
            var refreshToken = _jwtService.GenerateRefreshToken(payload.UserId);
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                throw new Exception("Failed to generate tokens.");
            }
            return new AuthResponseDto.LoginResponse
            {
                AccessToken = accessToken,
                User = new UserResponseDto
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                },
                RefreshToken = refreshToken

            };
        }

        public async Task<AuthResponseDto.RefreshTokenResponse> RefreshTokenAsync(string refreshToken)
        {
            var principal = _jwtService.ValidateRefreshToken(refreshToken);
            var userId = principal?.FindFirst("userId")?.Value ?? "";
            if (principal == null || principal.FindFirst("userId")?.Value != userId.ToString())
            {
                throw new Exception("Missing refresh token or Invalid refresh token.");
            }
            var user = await _authRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            if (user.Id != userId)
            {
                throw new Exception("Invalid refresh token for the user.");
            }

            var newAccessToken = _jwtService.GenerateToken(new JwtPayloadDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            });

            var newRefreshToken = _jwtService.GenerateRefreshToken(userId);

            if (string.IsNullOrEmpty(newAccessToken) || string.IsNullOrEmpty(newRefreshToken))
            {
                throw new Exception("Failed to generate tokens.");
            }

            return new AuthResponseDto.RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                User = new UserResponseDto
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                }
            };
        }

        public async Task<AuthResponseDto.LogoutResponse> LogoutAsync(string refreshToken)
        {
            // In a real application, you would invalidate the refresh token in the database or cache.
            var principal = _jwtService.ValidateRefreshToken(refreshToken);
            var userId = principal?.FindFirst("userId")?.Value ?? "";
            if (principal == null || principal.FindFirst("userId")?.Value != userId.ToString())
            {
                throw new Exception("Invalid refresh token.");
            }

            return new AuthResponseDto.LogoutResponse
            {
                Message = "User logged out successfully.",
                UserId = userId
            };
        }

    }
}
