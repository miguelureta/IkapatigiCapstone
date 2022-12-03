using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class PlantDiseaseImageView
    {
        [Display(Name = "Disease Name")]
        public string? DiseaseName { get; set; } = null!;
        //[Display(Name = "Disease Image")]
        //public IFormFile? PdImage { get; set; } = null!;
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }
    }
}
