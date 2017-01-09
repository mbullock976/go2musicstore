namespace Go2MusicStore.API.Interfaces
{
    using System;

    using Go2MusicStore.API.Interfaces.Managers;

    public interface IApplicationManager : IDisposable
    {
        IStoreManager StoreManager { get;  }
    }
}
