using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class ForgotPasswordRequestModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
