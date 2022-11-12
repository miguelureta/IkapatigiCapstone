using System.ComponentModel.DataAnnotations.Schema;
namespace IkapatigiCapstone.Models
{
    public class CreatePostReplyModel
    {
        public string Content { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
    }
}
