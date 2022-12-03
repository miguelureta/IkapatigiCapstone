using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkapatigiCapstone.Models
{
    public class CreatePostImageModel
    {
        [Required(ErrorMessage ="Title Required")]
        [MaxLength(99, ErrorMessage = "Title can only be 100 characters long")]
        public string? Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Tell something about your title.")]
        [MaxLength(499, ErrorMessage = "Content can only be 500 characters long")]
        public string? Content { get; set; }

        [ForeignKey("Forum")]
        public int? ForumId { get; set; }
        [NotMapped]
        [Display(Name = "Post Image")]
        public IFormFile? PdImage { get; set; }
    }
}
