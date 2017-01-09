namespace Go2MusicStore.Platform.Interfaces.DataLayer
{
    using System;
    using System.Linq;

    using Go2MusicStore.Models;
    using Go2MusicStore.Platform.Interfaces.DataLayer.Repositories;

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;

        void Save();

        IQueryable<ApplicationUser> ApplicationUsers { get; }
    }
}