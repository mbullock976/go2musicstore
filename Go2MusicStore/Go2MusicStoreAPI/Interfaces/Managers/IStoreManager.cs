namespace Go2MusicStore.API.Interfaces.Managers
{
    using System;
    public interface IStoreManager : IDisposable
    {        
        IAlbumManager AlbumManager { get; }

        IStoreAccountManager StoreAccountManager { get; }

        ISecurityManager SecurityManager { get; }
    }
}