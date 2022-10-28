using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<User> Users { get; set; } = new List<User>();
    }
}
