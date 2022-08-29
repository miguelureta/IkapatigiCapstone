using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class Tag
    {
        [Key]
        public int TagID { get; set; }

        [Required (ErrorMessage ="Required")]
        [Display(Name = "Tag Name")]
        public string TagName { get; set; } 

        public int UserID { get; set; }
    }
}
