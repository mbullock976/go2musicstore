[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Go2MusicStore.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Go2MusicStore.App_Start.NinjectWebCommon), "Stop")]

namespace Go2MusicStore.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.DependencyResolver.Implementation;
    using Go2MusicStore.Platform.DependencyResolver.Implementation;
    using Go2MusicStore.Platform.DependencyResolver.Modules;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            //var dependencyResolver = new PublicDependencyResolver();
            //var modules = dependencyResolver.GetModules();

            //var kernel = new StandardKernel();
            //try
            //{
            //    kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            //    kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            //    RegisterServices(kernel);
            //    RegisterServicesForWebAPI(kernel);

            //    return kernel;
            //}
            //catch
            //{
            //    kernel.Dispose();
            //    throw;
            //}

            //Share IOC Container with both MVC and WEB API
            //http://blog.developers.ba/simple-way-share-container-mvc-web-api/
            NinjectModule registrations = new PublicPlatformNinjectModule();
            var kernel = new StandardKernel(registrations);

            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            //MVC
            System.Web.Mvc.IDependencyResolver ninjectResolver = new PublicDependencyResolver(kernel);
            System.Web.Mvc.DependencyResolver.SetResolver(ninjectResolver); // MVC

            //WEB API
            GlobalConfiguration.Configuration.DependencyResolver = (System.Web.Http.Dependencies.IDependencyResolver)ninjectResolver;

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {            
        }

        private static void RegisterServicesForWebAPI(IKernel kernel)
        {            
        }

    }
}
