using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Model
{
    public class Comments
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Timestamp { get; set; }
        public Post Post { get; set; }
    }
}