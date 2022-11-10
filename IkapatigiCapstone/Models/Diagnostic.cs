using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkapatigiCapstone.Models
{
    public partial class Diagnostic
    {
        [Key]
        public int DiagnosticsId { get; set; }
        public int? PictureCollectionFromId { get; set; }
        [ForeignKey("Cure")]
        public int? CureId { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        [ForeignKey("Tag")]
        public int? TagId { get; set; }
        [ForeignKey("PlantDisease")]
        public int? PlantDiseaseId { get; set; }

        [Display(Name = "Cure")]
        public virtual Cure Cure { get; set; }
        [Display(Name = "Plant Disease")]
        public virtual PlantDisease PlantDisease { get; set; }
        [Display(Name = "Status")]
        public virtual Status Status { get; set; }
        [Display(Name = "Tag")]
        public virtual Tag Tag { get; set; }
        //Might use this ImageUrl for linking
        //public string ImageUrl { get; set; }
        [Display(Name = "Display Image")]
        [NotMapped]
        public IFormFile DisplayImage { get; set; }
    }
}
