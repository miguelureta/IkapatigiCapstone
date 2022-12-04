using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class DeleteReason
    {
        [Display(Name ="Reason for Deletion")]
        public string? Reason { get; set; }

    }
}
