using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class PostViewImageModel
    {
        [Display(Name ="Image")]
        public IEnumerable<string> Images { get; set; }
        public virtual PostImage _PostImage { get; set; }
    }
}
