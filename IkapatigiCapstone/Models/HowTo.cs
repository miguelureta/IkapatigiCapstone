using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;


namespace IkapatigiCapstone.Models
{
    public class HowTo
    {
        [Key]
        public int HowTosID { get; set; }

        [Required (ErrorMessage = "Required")]

        public string Title { get; set; }


        [Required(ErrorMessage = "Required")]
        [DataType (DataType.MultilineText)]
        public string Description { get; set; }

       
        public int? LikeCount { get; set; }

        public int? DislikeCount { get; set; }

        public Availability IsPublic { get; set; }

        public int? UserID { get; set; }

        public int? StatusID { get; set; }

        public int? PictureCollectionFromID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }


        public virtual Status Status { get; set; } = null!;
    }


    public enum Availability
    {
        MembersOnly = 1,
        IsPublic = 2
        
    }
}

