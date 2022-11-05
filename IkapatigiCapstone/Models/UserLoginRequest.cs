using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class UserLoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        //[BindProperty]
        //public string LoginError { get; set; }
    }
}
