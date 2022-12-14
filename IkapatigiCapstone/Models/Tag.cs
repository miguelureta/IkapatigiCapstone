using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public partial class Tag
    {
        public Tag()
        {
            Diagnostics = new HashSet<Diagnostic>();
            PlantDiseases = new HashSet<PlantDisease>();
        }

        public int TagId { get; set; }
        public string? TagName { get; set; }
        [Display(Name="User")]
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Diagnostic> Diagnostics { get; set; }
        public virtual ICollection<PlantDisease> PlantDiseases { get; set; }
    }
}
