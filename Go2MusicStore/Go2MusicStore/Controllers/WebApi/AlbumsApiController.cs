namespace Go2MusicStore.Controllers.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Common;
    using Go2MusicStore.Models;

    using WebGrease.Css.Extensions;

    public class AlbumsApiController : BaseApiController
    {
        
        public AlbumsApiController(IApplicationManager applicationManager)
            : base(applicationManager)
        {         
        }

        [HttpGet]
        [Route("api/v1/AlbumsApi/{id?}")]
        public Album Get(int? albumId)
        {
            return this.AlbumManager.GetById<Album>(albumId).CalculateTotalStarRating();
        }

        [HttpGet]
        [Route("api/v1/AlbumsApi/GetLatestAlbums/{count?}")]
        public IEnumerable<Album> GetLatestAlbums([FromUri] int? count)
        {
            if (count.HasValue)
            {
                return this.AlbumManager.Get<Album>().OrderByDescending(m => m.ReleaseDate).Take(10)
                    .CalculateTotalStarRating(); 
            }

            return this.AlbumManager.Get<Album>().CalculateTotalStarRating();
        }

        [HttpGet]
        [Route("api/v1/AlbumsApi/GetAlbumsByGenre/{genreId?}")]
        public IEnumerable<Album> GetAlbumsByGenre([FromUri] int? genreId)
        {
            if (genreId.HasValue)
            {
                return
                    this.AlbumManager.Get<Album>()
                        .Where(m => m.Artist.GenreId == genreId.Value)
                        .CalculateTotalStarRating();
            }

            return new List<Album>();

        }

        [HttpPost]
        [Route("api/v1/AlbumsApi/")]
        public void Post([FromUri] string imageSrc)
        {
            System.Web.HttpFileCollection httpRequest = System.Web.HttpContext.Current.Request.Files;
            for (int i = 0; i <= httpRequest.Count - 1; i++)
            {
                System.Web.HttpPostedFile postedfile = httpRequest[i];
                if (postedfile.ContentLength > 0)
                {                
                }
            }
        }

    }
}