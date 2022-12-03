using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class DiagnosticViewModel
    {
        [Display(Name="Cure")]
        public IEnumerable<Cure> CureList { get; set; }
        [Display(Name = "Disease")]
        public IEnumerable<PlantDisease> DiseaseList{ get; set; }
        [Display(Name = "Tags")]
        public IEnumerable<Tag> TagsList { get; set; }
        [Display(Name = "Status")]
        public IEnumerable<Status> StatusList { get; set; }
        public IEnumerable<Diagnostic> Diagnostic { get; set; }

        //[Display(Name = "Number of Clicks")]
        //public IEnumerable<int> Clicks { get; set; }
        //[Display(Name ="Image")]
        //public IEnumerable<Image> Images { get; set; }

    }
}
