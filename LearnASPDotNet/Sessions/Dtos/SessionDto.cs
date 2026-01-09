using System.ComponentModel.DataAnnotations;

namespace LearnASPDotNet.Sessions.Dtos
{
    public class SessionDto
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string RefreshToken { get; set; } = null!;
    }
}
