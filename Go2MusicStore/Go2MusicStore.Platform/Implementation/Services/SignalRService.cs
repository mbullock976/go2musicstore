namespace Go2MusicStore.Platform.Implementation.Services
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNet.SignalR;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Services;
    using Go2MusicStore.Platform.Implementation.Hubs;
    using Go2MusicStore.Platform.Implementation.Managers;
    using Go2MusicStore.Platform.Interfaces.DataLayer;

    public class SignalRService : ISignalRService
    {    
        private StoreHub storeHub;

        public SignalRService()
        {
            this.InitialiseHubs();
        }        

        public void OutofStockSignal(int albumId, bool isOutOfStock)
        {
            this.storeHub.StockCheckerSignal(albumId, isOutOfStock);
        }

        private void InitialiseHubs()
        {
            this.storeHub = new StoreHub();
        }
    }
}