namespace LearnASPDotNet.Features.Auths.Dtos
{
    public class UserResponse
    {
        public string UserId { get; set; } = string.Empty!;
        public string Email { get; set; } = string.Empty!;
        public string Username { get; set; } = string.Empty!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
