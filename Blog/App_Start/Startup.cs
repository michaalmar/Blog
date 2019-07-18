using Blog.DAL;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Blog.App_Start.Startup))]

namespace Blog.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.CreatePerOwinContext(BlogContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

        }
    }
}

