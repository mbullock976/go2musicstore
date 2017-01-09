namespace Go2MusicStore.Platform.Implementation.Hubs
{
    using Microsoft.AspNet.SignalR;

    using Go2MusicStore.API.Interfaces.Managers;

    public class StoreHub : Hub
    {
        public void StockCheckerSignal(int albumId, bool isOutOfStock)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<StoreHub>();
            if (hubContext != null)
            {
                hubContext.Clients.All.notifyStock(albumId, isOutOfStock);
            }
        }
    }
}