using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class PostViewImageModel
    {
        [Display(Name = "Post Image")]
        public string? ImageUrl { get; set; }
    }
}
