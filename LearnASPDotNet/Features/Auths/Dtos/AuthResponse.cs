using LearnASPDotNet.Features.Users.Dtos;

namespace LearnASPDotNet.Features.Auths.Dtos
{
    public class AuthResponse
    {
        public class LoginResponse
        {
            public string AccessToken { get; set; } = null!;
            public UserResponse User { get; set; } = null!;

            public string RefreshToken { get; set; } = null!;
        }

        public class RegisterResponse
        {
            public string Message { get; set; } = null!;
        }

        public class RefreshTokenResponse
        {
            public string AccessToken { get; set; } = null!;
            public UserResponse User { get; set; } = null!;
            public string RefreshToken { get; set; } = null!;
        }

        public class LogoutResponse
        {
            public string Message { get; set; } = null!;
            public string UserId { get; set; } = null!;
        }

    }

}
