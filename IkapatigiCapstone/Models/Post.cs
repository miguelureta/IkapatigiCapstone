using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class Post
    {
        public Post()
        {
            PostReplies = new HashSet<PostReply>();
        }

        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime Created { get; set; }
        public int? UserId { get; set; }
        public int? ForumId { get; set; }

        public virtual Forum? Forum { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<PostReply> PostReplies { get; set; }
    }
}
