using Blog.App_Start;
using Blog.IServices;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationUserManager userManager;
        private readonly ApplicationSignInManager signInManager;
        private readonly IAuthenticationManager authenticationManager;
        private readonly IUserService userService;
       

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager AuthenticationManager, IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationManager = AuthenticationManager;
            this.userService = userService;
        }

        // GET: Account
        public ActionResult Register()
        {
            ViewBag.RoleList = userService.GetRoles();

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

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                  
                    await userManager.AddToRoleAsync(user.Id, model.Role);
                    

                    return RedirectToAction("Index", "Home");
                }

              
                AddErrors(result);
            }

            var roleList = userService.GetRoles();
            ViewBag.RoleList = roleList;


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

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model,string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


    }
}
