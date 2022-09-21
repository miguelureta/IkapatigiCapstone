using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class Diagnostic
    {
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
    }
}
