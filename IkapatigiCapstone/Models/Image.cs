using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public partial class Image
    {
        public Image()
        {
            PlantDiseases = new HashSet<PlantDisease>(); 
        }
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public int PlantDiseaseID { get; set; }
        public int UserID { get; set; }

        public virtual ICollection<PlantDisease> PlantDiseases { get; set; }
        public virtual User? Users { get; set; }
    }
}
