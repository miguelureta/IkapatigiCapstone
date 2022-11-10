using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class PlantDisease
    {
        public PlantDisease()
        {
            Diagnostics = new HashSet<Diagnostic>();
        }

        public int PlantDiseaseId { get; set; }
        public string DiseaseName { get; set; } = null!;
        public byte[] ImageOfDisease { get; set; } = null!;
        public int? TagId { get; set; }
        public int? CureId { get; set; }
        public int? UserId { get; set; }

        public virtual Tag? Tag { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Diagnostic> Diagnostics { get; set; }
    }
}
