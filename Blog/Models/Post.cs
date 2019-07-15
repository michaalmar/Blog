using System;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public string AuthorName { get; set; }

        public byte[] Image { get; set; }

        //  public int UserId { get; set; }




    }
}