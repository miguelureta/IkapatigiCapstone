

namespace IkapatigiCapstone.Models
{
    public class Forum
    {
       public int ForumId { get; set; }
       public string Title { get; set; }

       public string Description { get; set; }

       public DateTime Created { get; set; }
       
       public string ImageUrl { get; set; }




       public virtual IEnumerable<Post> Posts { get; set; }

    }
}
