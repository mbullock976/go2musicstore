using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Go2MusicStore.Controllers.WebApi
{
    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;

    public abstract class BaseApiController : ApiController
    {
        protected BaseApiController(IApplicationManager applicationManager)
        {
            this.AlbumManager = applicationManager.StoreManager.AlbumManager;
            this.StoreAccountManager = applicationManager.StoreManager.StoreAccountManager;
            this.SecurityManager = applicationManager.StoreManager.SecurityManager;
        }

        public IStoreAccountManager StoreAccountManager { get; private set; }

        public IAlbumManager AlbumManager { get; private set; }

        public ISecurityManager SecurityManager { get; private set; }
    }
}
