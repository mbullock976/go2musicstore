using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Go2MusicStore.Controllers.WebApi
{
    using System.Web;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Models;

    public class ReviewsApiController : BaseApiController
    {

        public ReviewsApiController(IApplicationManager applicationManager)
            : base(applicationManager)
        {

        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Review newReviewModel)
        {
            if (newReviewModel.AlbumId == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, newReviewModel);
            }

            try
            {
                newReviewModel.ReviewDate = DateTime.UtcNow;
                this.AlbumManager.Add<Review>(newReviewModel);
                this.AlbumManager.Save();

            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, newReviewModel);
            }

            return Request.CreateResponse(HttpStatusCode.Created, newReviewModel);
        }
    }
}
