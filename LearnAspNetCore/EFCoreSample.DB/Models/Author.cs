using System.Collections.Generic;

namespace EFCoreSample.DB.Models
{
    public class Author
    {
        public Author()
        {
            Blogs = new List<Blog>();
        }

        public int Id
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string ProfilePhoto
        {
            get;
            set;
        }

        public List<Blog> Blogs
        {
            get;
            set;
        }
    }
}
