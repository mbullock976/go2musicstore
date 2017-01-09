namespace Go2MusicStore.Platform.Implementation.Managers
{
    using Go2MusicStore.API.Interfaces.Managers;

    public class StoreManager : IStoreManager
    {
        public StoreManager(
            IAlbumManager albumManager,
            IStoreAccountManager storeAccountManager,
            ISecurityManager securityManager)
        {
            this.StoreAccountManager = storeAccountManager;
            this.AlbumManager = albumManager;
            this.SecurityManager = securityManager;
        }

        public IAlbumManager AlbumManager { get; private set; }

        public IStoreAccountManager StoreAccountManager { get; private set; }

        public ISecurityManager SecurityManager { get; private set; }

        public void Dispose()
        {            
        }       
    }
}