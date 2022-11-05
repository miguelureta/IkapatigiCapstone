using System;
using System.Collections.Generic;

namespace IkapatigiCapstone
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
        public int? UserId { get; set; }

        public virtual ICollection<Diagnostic> Diagnostics { get; set; }
        public virtual ICollection<PlantDisease> PlantDiseases { get; set; }
    }
}
