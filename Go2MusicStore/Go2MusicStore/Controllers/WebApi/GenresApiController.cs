namespace Go2MusicStore.Controllers.WebApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Models;
    using Go2MusicStore.ViewModels;

    public class GenresApiController : BaseApiController
    {
        public GenresApiController(IApplicationManager applicationManager)
            : base(applicationManager)
        {

        }

        // GET: api/GenresApi
        public IEnumerable<GenreSummaryViewModel> Get()
        {
            return this.AlbumManager.Get<Genre>().Select(x=> new GenreSummaryViewModel
                                                             {
                                                                GenreId = x.GenreId,
                                                                Name = x.Name,
                                                                Description = x.Description                                                                
                                                             });
        }

        // GET: api/GenresApi/5
        public GenreSummaryViewModel Get(int? id)
        {
             var selectedGenre = this.AlbumManager.Get<Genre>().FirstOrDefault(m => m.GenreId == id);
             return new GenreSummaryViewModel
                        {
                            GenreId = selectedGenre.GenreId,
                            Name = selectedGenre.Name,
                            Description = selectedGenre.Description
                        };
        }

        // POST: api/GenresApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GenresApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GenresApi/5
        public void Delete(int id)
        {
        }
    }
}
