using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class Role
    {
        //Originally did not have this constructor
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string? Role1 { get; set; }

        //Below was previously public virtual User? User {get; set;}
        public virtual ICollection<User> Users { get; set; }
    }
}
