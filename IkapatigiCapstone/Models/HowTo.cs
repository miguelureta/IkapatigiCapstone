using System;
using System.Collections.Generic;

namespace IkapatigiCapstone
{
    public partial class HowTo
    {
        public int HowTosId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
        public int? IsPublic { get; set; }
        public int? UserId { get; set; }
        public int? StatusId { get; set; }
        public int? PictureCollectionFromId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? ArticleBody { get; set; }

        public virtual Status? Status { get; set; }
    }
}
