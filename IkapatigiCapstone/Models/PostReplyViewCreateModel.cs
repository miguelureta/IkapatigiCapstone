using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class PostReplyViewCreateModel
    {
        public IEnumerable<PostReply>? postReplies { get; set; }
        [Display(Name="Replies")]
        public string Content { get; set; }
        public int? PostID { get; set; }
        public virtual Post _Post { get; set; }
    }
}
