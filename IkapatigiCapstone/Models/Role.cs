using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public string? Role1 { get; set; }

        public virtual User? User { get; set; }
    }
}
