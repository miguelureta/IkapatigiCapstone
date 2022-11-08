namespace IkapatigiCapstone.Models
{
    public class PostReply
    {
        public int PostReplyId { get; set; }
        public string Content { get; set; }

        public DateTime Created { get; set; }

        public int? UserID { get; set; }
        public int? PostID { get; set; }
        
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
