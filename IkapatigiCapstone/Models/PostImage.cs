using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkapatigiCapstone.Models
{
    public class PostImage
    {
        public PostImage()
        {
            Posts = new HashSet<Post>();
        }
        [Key]
        public int PostImageID { get; set; }
        [Display(Name ="Image")]
        public string? ImageName { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        [ForeignKey("User")]
        public int? UserID { get; set; }
        //Changed foreignkey to PostId from PostID
        [ForeignKey("PostId")]
        public virtual ICollection<Post> Posts { get; set; }
        public virtual User? Users { get; set; }
    }
}
