using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnASPDotNet.Features.Auths.Dtos
{
    public class RegisterRequestDto
    {
        [Required]
        [DefaultValue("User")]
        public string Username { get; set; } = string.Empty!;

        [Required]
        [EmailAddress]
        [DefaultValue("user@gmail.com")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [MinLength(5, ErrorMessage = "Email must be at least 5 characters long.")]
        public string Email { get; set; } = string.Empty!;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DefaultValue("123456")]
        public string Password { get; set; } = string.Empty!;

        [DefaultValue("0xxxxxxxxx")]
        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 characters.")]
        public string? Phone { get; set; }
        [DefaultValue("123 Main St, City, Country")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }
    }
}
