using System;

namespace EFCoreSample.DB.Models
{
    public class Blog
    {
       public int Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public DateTime PostDate
        {
            get;
            set;
        }

        public Author Author
        {
            get;
            set;
        }

        public DateTime UpdateTime { get; set; }
    }
}
