using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public partial class PlantDisease
    {
        public PlantDisease()
        {
            Diagnostics = new HashSet<Diagnostic>();
            Images = new HashSet<Image>();
        }

        public int PlantDiseaseId { get; set; }
        [Display(Name ="Disease Name")]
        public string DiseaseName { get; set; } = null!;
        public byte[]? ImageOfDisease { get; set; } = null!;
        [Display(Name = "Tag")]
        public int? TagId { get; set; }
        public int? CureId { get; set; }
        [Display(Name ="User")]
        public int? UserId { get; set; }
        public int? NumberofClicks { get; set; }
        public virtual Tag? Tag { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Diagnostic> Diagnostics { get; set; }
    }
}
