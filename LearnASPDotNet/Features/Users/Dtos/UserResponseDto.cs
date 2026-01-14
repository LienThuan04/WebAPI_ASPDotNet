namespace LearnASPDotNet.Features.Users.Dtos
{
    public class UserResponseDto
    {
        public string UserId { get; set; } = string.Empty!;
        public string Email { get; set; } = string.Empty!;
        public string Username { get; set; } = string.Empty!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
