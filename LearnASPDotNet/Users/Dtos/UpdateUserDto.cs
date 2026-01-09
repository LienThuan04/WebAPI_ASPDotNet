
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnASPDotNet.Users.Dtos
{
    public class UpdateUserDto
    {
        [DefaultValue("0XXXXXXXXX")]
        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 characters.")]

        public string? phone { get; set; }
        [DefaultValue("123 XXXX")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]

        public string? address { get; set; }
        [DefaultValue("XXX@example.com")]
        [EmailAddress]
        public string? Email { get; set; }

    }
}
