using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace AuthApi.Dtos
{
    public class LoginUserDto
    {
        [Required]
        [DefaultValue("User")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DefaultValue("123456")]
        public string Password { get; set; } = string.Empty;
    }
}
