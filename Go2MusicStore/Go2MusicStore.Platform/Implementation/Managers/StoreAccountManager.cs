namespace Go2MusicStore.Platform.Implementation.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.API.Interfaces.Services;
    using Go2MusicStore.Platform.Interfaces.DataLayer;

    public class StoreAccountManager : DataManager, IStoreAccountManager
    {
        public StoreAccountManager(
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}