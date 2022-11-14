using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public partial class PostReply
    {
        public int PostReplyId { get; set; }
        public string? Content { get; set; }
        [Display(Name = "Created On")]
        public DateTime? Created { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
