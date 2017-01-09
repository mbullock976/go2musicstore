namespace Go2MusicStore.Platform.Interfaces.DataLayer.Factories
{
    using System.Data.Entity;

    using Go2MusicStore.Platform.Implementation.DataLayer;
    using Go2MusicStore.Platform.Interfaces.DataLayer.Repositories;

    public interface IRepositoryFactory
    {
        IGenericRepository<T> Create<T>(DbContext context) where T : class;
    }
}