namespace Go2MusicStore.Controllers.WebApi
{
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    [RoutePrefix("api/v1/AuthenticationApi")]
    public class AuthenticationApiController : ApiController
    {
        [Route("")]
        public HttpResponseMessage GetAuth()
        {
            return this.Request.CreateResponse(HttpStatusCode.Accepted, new
                          {
                              IsAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated,
                              Name = HttpContext.Current.User.Identity.Name
                          });
        }

        [Route("secured")]
        [Authorize]
        public HttpResponseMessage GetSecured()
        {
            return this.Request.CreateResponse(HttpStatusCode.Accepted, new
            {
                IsAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated,
                Name = HttpContext.Current.User.Identity.Name
            });
        }
    }
}