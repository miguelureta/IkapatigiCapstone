using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class PostReplyViewImageCreateModel
    {
        [Display(Name ="Image")]
        public IFormFile? postImage { get; set; }
        [Display(Name ="Description")]
        public string? ImageTextIn { get; set; }
    }
}
