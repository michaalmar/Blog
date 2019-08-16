﻿using AutoMapper;
using Blog.App_Start;
using Blog.DAL;
using Blog.IServices;
using Blog.Mapper;
using Blog.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]
namespace Blog.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            kernel.Bind<ApplicationUserManager>().ToMethod(ctx =>
            {
                var httpContext = ctx.Kernel.Get<HttpContextBase>();
                return httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            });

            kernel.Bind<ApplicationSignInManager>().ToMethod(ctx =>
            {
                var httpContext = ctx.Kernel.Get<HttpContextBase>();
                return httpContext.GetOwinContext().Get<ApplicationSignInManager>();

            });

            kernel.Bind<IAuthenticationManager>().ToMethod(ctx =>
            {
                var httpContext = ctx.Kernel.Get<HttpContextBase>();
                return httpContext.GetOwinContext().Authentication;
            });


            kernel.Bind<IMapper>().ToMethod(context =>
                 {
                     var config = new MapperConfiguration(cfg =>
                     {
                         cfg.AddProfile(new UserMappingProfile());
                         cfg.ConstructServicesUsing(t => kernel.Get(t));
                     });
                     return config.CreateMapper();
                 }).InSingletonScope();


            RegisterServices(kernel);
            return kernel;

        }

        private static void RegisterServices(IKernel kernel)
        {

         
            kernel.Bind<BlogContext>().ToSelf().InRequestScope();
            kernel.Bind<IPostsService>().To<PostsService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();


        }
    }
}
