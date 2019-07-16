using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class PostCreateViewModel
    {
        [Display(Name ="Tytuł")]
        public string Title { get; set; }


        [Display(Name ="Zawartość")]
        public string Content { get; set; }

        [Display(Name = "Autor")]
        public string AuthorName { get; set; }

        public DateTime GetDataTime()
        {
            return DateTime.Now;

        }

        public byte[] Image { get; set; }

    }
}
