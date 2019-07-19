using Blog.App_Start;
using Blog.DAL;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager AuthenticationManager;
        private readonly BlogContext DbContext;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager AuthenticationManager, BlogContext blogContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.AuthenticationManager = AuthenticationManager;
            DbContext = blogContext;
        }

        // GET: Account
        public ActionResult Register()
        {
            ViewBag.RoleList = new SelectList(DbContext.Roles.Where(u => !u.Name.Contains("Admin")).ToList(),"Name","Name");

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

                  
                    await _userManager.AddToRoleAsync(user.Id, model.Role);
                    

                    return RedirectToAction("Index", "Home");
                }

              
                AddErrors(result);
            }
            var roleLsit = new SelectList(DbContext.Roles.Where(u => !u.Name.Contains("Admin")).ToList(), "Name", "Name");

            ViewBag.RoleList = roleLsit;

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

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);

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
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


    }
}
