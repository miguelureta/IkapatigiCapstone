using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class AdminCreationRequest
    {
        [Required]
        [MaxLength(10)]
        public string Username { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
