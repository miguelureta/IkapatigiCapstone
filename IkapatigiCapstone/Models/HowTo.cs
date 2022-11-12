using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public partial class HowTo
    {
        [Key]
        public int HowTosID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        public string Description { get; set; }

        public int? LikeCount { get; set; }

        public int? DislikeCount { get; set; }
        [Display(Name ="Available For")]
        public Availability? IsPublic { get; set; }

        public int? UserID { get; set; }

        //public StatusChange? StatusID { get; set; }

        public int? StatusID { get; set; }

        public int? PictureCollectionFromID { get; set; }
        [Display(Name ="Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name ="Date Updated")]
        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Content")]
        [MaxLength(5000)]
        [DataType(DataType.MultilineText)]
        public string? ArticleBody { get; set; }
        public virtual Status? Status { get; set; } = null!;
        public virtual User? User { get; set; }

    }
    public enum Availability
    {
        MembersOnly = 1,
        IsPublic = 2
    }
    //public virtual Status? Status { get; set; }
    //public enum StatusChange
    //{
    //    MembersOnly = 1,
    //    IsPublic = 2

    //}
    
}