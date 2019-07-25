using Blog.DAL;
using Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartup(typeof(Blog.App_Start.Startup))]

namespace Blog.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            CreateUserRoles();

            app.CreatePerOwinContext(BlogContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }

        private void CreateUserRoles()
        {
            BlogContext context = new BlogContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if(!roleManager.RoleExists(Constant.Constant.User.ROLE_ADMIN))
            {
                var role = new IdentityRole();
                role.Name = Constant.Constant.User.ROLE_ADMIN;
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists(Constant.Constant.User.ROLE_READER))
            {
                var role = new IdentityRole();
                role.Name = Constant.Constant.User.ROLE_READER;
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists(Constant.Constant.User.ROLE_EDITOR)) 
            {
                var role = new IdentityRole();
                role.Name = Constant.Constant.User.ROLE_EDITOR;
                roleManager.Create(role);
            }


        }


    }
}

