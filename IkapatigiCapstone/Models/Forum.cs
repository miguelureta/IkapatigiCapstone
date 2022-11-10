using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class Forum
    {
        public Forum()
        {
            Posts = new HashSet<Post>();
        }

        public int ForumId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public string? ImageUrl { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
