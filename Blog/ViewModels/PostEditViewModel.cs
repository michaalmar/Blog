using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class PostEditViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Tytuł")]
        public string Title { get; set; }


        [Display(Name ="Treść")]
        public string Content { get; set; }

    }
}