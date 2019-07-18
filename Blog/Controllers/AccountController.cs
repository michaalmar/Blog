using Blog.App_Start;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {

        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserViewModel model)
        {

            if (ModelState.IsValid)
            {


                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }
            return View(model);
        }
    
      
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

    }
}