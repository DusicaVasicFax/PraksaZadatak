using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Model
{
    public class User : IdentityUser
    {
        public string Image { get; set; }
        public string Lastname { get; set; }
        public bool Deleted { get; set; }
        public virtual List<Post> posts { get; set; }
    }
}