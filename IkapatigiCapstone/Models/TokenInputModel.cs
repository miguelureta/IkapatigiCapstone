using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class TokenInputModel
    {
        [Required]
        [Display(Name ="Given Token")]
        public string TokenIn { get; set; }
        [Required, EmailAddress]
        [Display(Name = "Requested Email")]
        public string EmailIn { get; set; }
    }
}
