using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class AdminLoginRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        //[BindProperty]
        //public string LoginError { get; set; }
    }
}
