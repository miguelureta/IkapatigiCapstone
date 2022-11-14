using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class ModeratorCreationModel
    {
        public string Username { get; set; }
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}
