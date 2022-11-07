using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkapatigiCapstone.Models
{
    public class UserManagementViewModel
    {
        [Display(Name ="User Email")]
        public string Email { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Status")]
        [ForeignKey("StatusID")]
        public string Status { get; set; }
        public IEnumerable<User> User { get; set; }
    }
}
