using System.ComponentModel.DataAnnotations;


namespace IkapatigiCapstone.Models
{
    public class UserRegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage ="Please enter at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required,Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
