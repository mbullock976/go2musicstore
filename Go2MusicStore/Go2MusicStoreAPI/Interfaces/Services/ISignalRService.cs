namespace Go2MusicStore.API.Interfaces.Services
{
    using System.Threading.Tasks;

    public interface ISignalRService
    {
        void OutofStockSignal(int albumId, bool isOutOfStock);
    }
}