
namespace Session.Dtos
{
    public class SessionDto
    {
        public string UserId { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRE_DAYS") ?? "7"));

    }
}
