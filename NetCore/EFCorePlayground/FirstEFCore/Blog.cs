using System;
using System.Collections.Generic;

namespace FirstEFCore
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public DateTime CreatDate { get; set; }

        public List<Post> Posts { get; set; }
    }
}