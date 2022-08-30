using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class PlantDiseasesNoCure
    {
        public int PlantDiseaseId { get; set; }
        public string DiseaseName { get; set; } = null!;
        public byte[]? ImageOfDisease { get; set; }
        public int? TagId { get; set; }
        public int? CureId { get; set; }
    }
}
