using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        public string Email { get; set; }

    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

}
