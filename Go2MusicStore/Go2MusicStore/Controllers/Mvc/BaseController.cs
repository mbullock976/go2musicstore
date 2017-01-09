using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Go2MusicStore.Controllers.Mvc
{
    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;

    public abstract class BaseController : Controller
    {
        protected BaseController(IApplicationManager applicationManager)
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