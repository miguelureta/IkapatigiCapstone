namespace IkapatigiCapstone.Models
{
    public class PostImage
    {
        public PostImage()
        {
            Posts = new HashSet<Post>();
        }
        public int PostImageID { get; set; }
        public string? ImageName { get; set; }
        public int? PostID { get; set; }
        public int? UserID { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual User? Users { get; set; }
    }
}
