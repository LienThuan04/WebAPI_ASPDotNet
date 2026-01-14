using System.ComponentModel.DataAnnotations;

namespace LearnASPDotNet.Features.Sessions.Dtos
{
    public class CreateSessionDto
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string RefreshToken { get; set; } = null!;
    }
}
