using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class PostReply
    {
        public int PostReplyId { get; set; }
        public string? Content { get; set; }
        public DateTime? Created { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
