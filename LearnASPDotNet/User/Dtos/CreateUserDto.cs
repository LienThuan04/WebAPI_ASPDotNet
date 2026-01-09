using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace User.Dtos
{
    public class CreateUserDto
    {
        [Required]
        [DefaultValue("User")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [DefaultValue("user@gmail.com")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DefaultValue("123456")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [DefaultValue("0999999999")]
        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 characters.")]
        public string? Phone { get; set; }

        [DefaultValue("123 Main St, City, Country")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }
    }
}
