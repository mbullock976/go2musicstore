namespace Go2MusicStore.API.Tests
{
    using System.Web.Mvc;

    using Go2MusicStore.DependencyResolver.Implementation;
    using Go2MusicStore.Platform.DependencyResolver.Implementation;
    using Go2MusicStore.Platform.DependencyResolver.Modules;

    using Ninject;
    using Ninject.Modules;

    public class TestClassBase
    {
        private readonly StandardKernel kernel;

        public TestClassBase()
        {
            NinjectModule registrations = new PublicPlatformNinjectModule();
            this.kernel = new StandardKernel(registrations);
            IDependencyResolver ninjectResolver = new PublicDependencyResolver(kernel);

            System.Web.Mvc.DependencyResolver.SetResolver(ninjectResolver); // MVC
            //GlobalConfiguration.Configuration.DependencyResolver = (System.Web.Http.Dependencies.IDependencyResolver)ninjectResolver; // Web API           
        }

        // Only use this when you want a real instance from IOC container
        // use this for integration testing
        public T ResolveInstance<T>()
        {
            return this.kernel.Get<T>();
        }
    }
}