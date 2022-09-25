using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.ViewModel
{
    public class RegisterModViewModel
    {
        public RegisterModViewModel()
        {
            Username = "";
            Password = "";
            RoleId = 0;
        }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
