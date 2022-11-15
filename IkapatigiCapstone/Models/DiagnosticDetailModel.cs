using System.ComponentModel.DataAnnotations;
namespace IkapatigiCapstone.Models
{
    public class DiagnosticDetailModel
    {
        [Display(Name ="Cure")]
        public Cure cure { get; set; }
        [Display(Name = "Plant Disease")]
        public PlantDisease disease { get; set; }
        [Display(Name = "Description")]
        public Tag tag { get; set; }
        [Display(Name = "Status")]
        public Status status { get; set; }
    }
}
