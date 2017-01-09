namespace Go2MusicStore.Platform.Implementation.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Go2MusicStore.API;
    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Platform.Interfaces.DataLayer;

    public class AlbumManager : DataManager, IAlbumManager
    {
        public AlbumManager(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {            
        }
    }
}