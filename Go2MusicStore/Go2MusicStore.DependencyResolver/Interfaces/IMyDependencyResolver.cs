namespace Go2MusicStore.DependencyResolver.Interfaces
{
    using Ninject.Modules;

    public interface IMyDependencyResolver
    {
        INinjectModule[] GetModules();
    }
}