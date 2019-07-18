using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resources))]
        public string UserName { get; set; }

        [Required]
        [Display(Name ="Password",ResourceType =typeof(Resources.Resources))]
        public string Password { get; set; }

        [Required]
        [Display(Name ="ConfirmPassword",ResourceType =typeof(Resources.Resources))]
        [Compare("Password",ErrorMessage ="Podane hasła różnią się od siebie!")]
        public string ConffirmPassword { get; set; }

    }
}


