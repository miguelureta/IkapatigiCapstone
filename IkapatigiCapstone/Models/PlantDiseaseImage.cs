using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkapatigiCapstone.Models
{
    public class PlantDiseaseImage
    {
        [Display(Name = "Disease Name")]
        [Required]
        public string DiseaseName { get; set; } = null!;
        //public byte[]? ImageOfDisease { get; set; } = null!;
        [Display(Name = "Tag")]
        [Required]
        public int? TagId { get; set; }
        [Display(Name = "Cure")]
        [Required]
        public int? CureId { get; set; }
        [NotMapped]
        [Display(Name = "Disease Image")]
        [Required(ErrorMessage ="Disease image required")]
        public IFormFile? PdImage { get; set; }
    }
}
