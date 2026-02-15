using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace LearnASPDotNet.Features.Auths.Dtos.Attribute
{
    public class UsernameOrEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Username or Email is required.");

            var input = value.ToString()!.Trim();

            // Nếu có dấu @ thì kiểm tra email
            if (input.Contains("@"))
            {
                if (!IsValidEmail(input))
                    return new ValidationResult("Invalid email format.");
            }
            else
            {
                // Validate username (ví dụ: chỉ chữ + số, 3-20 ký tự)
                var usernameRegex = new Regex("^[a-zA-Z0-9]{3,20}$");

                if (!usernameRegex.IsMatch(input))
                    return new ValidationResult("Username must be 3-20 characters and contain only letters or numbers.");
            }

            return ValidationResult.Success;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email); // Sử dụng MailAddress để kiểm tra định dạng email
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
