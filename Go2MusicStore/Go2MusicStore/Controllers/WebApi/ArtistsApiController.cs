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
    using Go2MusicStore.Models;

    public class ArtistsApiController : BaseApiController
    {

        public ArtistsApiController(IApplicationManager applicationManager)
            : base(applicationManager)
        {
            
        }

        public Artist Get(int? artistId)
        {
            return this.AlbumManager.GetById<Artist>(artistId);
        }
    }
}
