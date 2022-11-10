using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkapatigiCapstone.Models
{
    public partial class Forum
    {
        public Forum()
        {
            Posts = new HashSet<Post>();
        }

        public int ForumId { get; set; }
        [Display(Name = "Forum Title")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Date Created")]
        public DateTime Created { get; set; }
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
