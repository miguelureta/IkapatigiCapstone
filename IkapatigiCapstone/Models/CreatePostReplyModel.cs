using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IkapatigiCapstone.Models
{
    public class CreatePostReplyModel
    {
        [MaxLength(99, ErrorMessage = "Title can only be 100 characters long")]
        public string Content { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
    }
}
