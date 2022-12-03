using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkapatigiCapstone.Models
{
    public partial class Post
    {
        public Post()
        {
            PostReplies = new HashSet<PostReply>();
        }
        [Key]
        public int PostId { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Content { get; set; }
        [Display(Name ="Created On")]
        public DateTime Created { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        [ForeignKey("Forum")]
        public int? ForumId { get; set; }
        
        public virtual Forum? Forum { get; set; }
        public virtual User? User { get; set; }
        //[ForeignKey("PostID")]
        public virtual ICollection<PostReply> PostReplies { get; set; }
    }
}
