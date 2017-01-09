namespace Go2MusicStore.Platform.DependencyResolver.Modules
{
    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.API.Interfaces.Services;
    using Go2MusicStore.Platform.Implementation.DataLayer;
    using Go2MusicStore.Platform.Implementation.DataLayer.Factories;
    using Go2MusicStore.Platform.Implementation.Managers;
    using Go2MusicStore.Platform.Implementation.Services;
    using Go2MusicStore.Platform.Interfaces.DataLayer;
    using Go2MusicStore.Platform.Interfaces.DataLayer.Factories;

    using Ninject.Modules;
    using Ninject.Web.Common;

    public class PublicPlatformNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositoryFactory>().To<RepositoryFactory>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<ISignalRService>().To<SignalRService>();
            Bind<ISecurityManager>().To<SecurityManager>();
            Bind<IStoreAccountManager>().To<StoreAccountManager>();
            Bind<IAlbumManager>().To<AlbumManager>();
            Bind<IStoreManager>().To<StoreManager>();
            Bind<IApplicationManager>().To<ApplicationManager>().InRequestScope();
        }
    }
}