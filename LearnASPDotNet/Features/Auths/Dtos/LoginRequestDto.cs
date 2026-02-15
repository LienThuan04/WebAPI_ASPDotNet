using LearnASPDotNet.Features.Auths.Dtos.Attribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnASPDotNet.Features.Auths.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        [DefaultValue("User")]
        [UsernameOrEmail] // Custom validation attribute to allow either username or email
        public string UsernameOrEmail { get; set; } = string.Empty!;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DefaultValue("123456")]
        public string Password { get; set; } = string.Empty;
    }
}
