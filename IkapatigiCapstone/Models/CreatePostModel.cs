using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IkapatigiCapstone.Models
{
    public class CreatePostModel
    {
        [MaxLength(99, ErrorMessage = "Title can only be 100 characters long")]
        public string Title { get; set; }
        [MaxLength(499, ErrorMessage = "Content can only be 500 characters long")]
        public string Content { get; set; }
        [ForeignKey("Forum")]
        public int? ForumId { get; set; }
        public virtual Forum forum { get; set; }
    }
}
