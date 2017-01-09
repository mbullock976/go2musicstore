namespace Go2MusicStore.Platform.Implementation.Managers
{
    using System;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    
    public class ApplicationManager : IApplicationManager
    {
        private bool isDisposed = false;

        public ApplicationManager(IStoreManager storeManager)
        {
            this.StoreManager = storeManager;
        }

        public IStoreManager StoreManager { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);   
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                if (isDisposing)
                {
                    //TODO dispose
                }

                this.isDisposed = true;
            }
        }      
    }
}