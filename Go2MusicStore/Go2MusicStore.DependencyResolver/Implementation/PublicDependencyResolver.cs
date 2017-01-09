namespace Go2MusicStore.Platform.DependencyResolver.Implementation
{
    using System.Web.Http.Dependencies;

    using Go2MusicStore.DependencyResolver.Implementation;
    using Go2MusicStore.Platform.DependencyResolver.Modules;

    using Ninject;
    using Ninject.Modules;

    public class PublicDependencyResolver : NinjectDependencyScope, IDependencyResolver, System.Web.Mvc.IDependencyResolver  
    {        

        private readonly IKernel kernel;

        public PublicDependencyResolver(IKernel kernel)
           : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(this.kernel.BeginBlock());
        }

        public INinjectModule[] GetModules()
        {
            var modules = new INinjectModule[]
            {
                new PublicPlatformNinjectModule()
            };
            return modules;
        }
    }
}