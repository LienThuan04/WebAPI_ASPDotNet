using LearnASPDotNet.Features.Auths.Dtos;

namespace LearnASPDotNet.Features.Auths
{
    public interface IAuthService
    {
        Task<AuthResponseDto.LoginResponse> LoginAsync(LoginRequestDto loginRequest);
        Task<AuthResponseDto.RegisterResponse> RegisterAsync(RegisterRequestDto registerRequest);
        Task<AuthResponseDto.RefreshTokenResponse> RefreshTokenAsync(string refreshToken);
        Task<AuthResponseDto.LogoutResponse> LogoutAsync(string refreshToken);
    }
}
