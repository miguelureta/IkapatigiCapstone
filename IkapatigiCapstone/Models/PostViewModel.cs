using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public class PostViewModel
    {
        [Display(Name="Forum Posts")]
        public IEnumerable<Post> Posts { get; set; }
        public virtual Forum _Forum { get; set; }

    }
}
