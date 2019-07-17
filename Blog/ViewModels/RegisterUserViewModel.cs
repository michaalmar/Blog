using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [Display(Name ="Nazwa Użytkownika")]
        public string UserName { get; set; }

        [Required]
        [Display(Name ="Hasło")]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Potwierdź hasło")]
        [Compare("Password",ErrorMessage ="Podane hasła różnią się od siebie!")]
        public string ConffirmPassword { get; set; }

    }
}