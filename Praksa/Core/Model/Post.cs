using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Model
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Image { get; set; }
        public List<Comments> Comments { get; set; }
        public virtual User user { get; set; }
    }
}