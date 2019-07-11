using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public string AuthorName { get; set; }
    }
}