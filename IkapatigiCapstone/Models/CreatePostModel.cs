using System.ComponentModel.DataAnnotations.Schema;
namespace IkapatigiCapstone.Models
{
    public class CreatePostModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        [ForeignKey("Forum")]
        public int? ForumId { get; set; }
        public virtual Forum forum { get; set; }
    }
}
