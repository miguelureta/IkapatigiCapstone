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
        public int? CureId { get; set; }
        public int StatusId { get; set; }
        public int? TagId { get; set; }
        public int? PlantDiseaseId { get; set; }

        public virtual Cure? Cure { get; set; }
        public virtual PlantDisease? PlantDisease { get; set; }
        public virtual Status Status { get; set; } = null!;
        public virtual Tag? Tag { get; set; }
        //Might use this ImageUrl for linking
        //public string ImageUrl { get; set; }
        [Display(Name="Display Image")]
        [NotMapped]
        public IFormFile DisplayImage { get; set; }
    }
}
