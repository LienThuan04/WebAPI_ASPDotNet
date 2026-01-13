using LearnASPDotNet.Features.Auths.Dtos;

namespace LearnASPDotNet.Features.Auths
{
    public interface IAuthService
    {
        Task<AuthResponse.LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<AuthResponse.RegisterResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<AuthResponse.RefreshTokenResponse> RefreshTokenAsync(string refreshToken);
        Task<AuthResponse.LogoutResponse> LogoutAsync(string refreshToken);
    }
}
