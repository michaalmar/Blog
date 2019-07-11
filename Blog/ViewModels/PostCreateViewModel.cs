using System;

namespace Blog.ViewModels
{
    public class PostCreateViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

        public DateTime GetDataTime()
        {
            return DateTime.Now;

        }
    }
}
