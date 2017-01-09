namespace Go2MusicStore.Controllers.Mvc
{
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Models;

    public class ArtistsController : BaseController
    {        
        public ArtistsController(IApplicationManager applicationManager)
            :base(applicationManager)
        {        
        }

        // GET: Artists
        public ActionResult Index(int? SelectedGenre)
        {
            var genres = this.AlbumManager
                .Get<Genre>(orderBy: q => q.OrderBy(d => d.Name)).ToList();

            this.ViewBag.SelectedGenre = new SelectList(genres, "GenreId", "Name", SelectedGenre);
            int genreId = SelectedGenre.GetValueOrDefault();

            IQueryable<Artist> artists =
                this.AlbumManager.Get<Artist>(includeProperties: "Genre")
                    .Where(c => !SelectedGenre.HasValue || c.GenreId == genreId)
                    .OrderBy(d => d.ArtistId)
                    .AsQueryable();

            //put breakpoint here to see the raw sql sent to database from EF
            //var sql = artists.ToString();

            return this.View(artists.ToList());
        }

        // GET: Genre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Artist artist = this.AlbumManager.GetById<Artist>(id);

            if (artist == null)
            {
                return this.HttpNotFound();
            }

            return this.View(artist);
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            this.PopulateGenreDropDownList();
            return this.View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,GenreId")] Artist artist)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this.AlbumManager.Add<Artist>(artist);
                    this.AlbumManager.Save();

                    return this.RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                // Log the error (uncomment dex variable name and add a line here to write a log.)
                this.ModelState.AddModelError(
                    string.Empty,
                    "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            this.PopulateGenreDropDownList(artist.GenreId);
            return this.View(artist);
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Artist artist = this.AlbumManager.GetById<Artist>(id);
            
            if (artist == null)
            {
                return this.HttpNotFound();
            }

            this.PopulateGenreDropDownList(artist.GenreId);
            return this.View(artist);
        }

        // POST: Artist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var artistToUpdate = this.AlbumManager.GetById<Artist>(id);

            if (this.TryUpdateModel(artistToUpdate, string.Empty, new[] { "Name", "Description", "StartDate", "GenreId" }))
            {
                try
                {
                    this.AlbumManager.Save();

                    return this.RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    // Log the error (uncomment dex variable name and add a line here to write a log.
                    this.ModelState.AddModelError(
                        string.Empty,
                        "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            this.PopulateGenreDropDownList(artistToUpdate.GenreId);
            return this.View(artistToUpdate);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Artist artist = this.AlbumManager.GetById<Artist>(id);

            if (artist == null)
            {
                return this.HttpNotFound();
            }

            return this.View(artist);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = this.AlbumManager.GetById<Artist>(id);
            this.AlbumManager.Delete(artist);
            this.AlbumManager.Save();

            return this.RedirectToAction("Index");
        }

        private void PopulateGenreDropDownList(object selectedGenre = null)
        {
            var genresQuery =
                this.AlbumManager
                .Get<Genre>(orderBy: m => m.OrderBy(p => p.Name))
                .Select(m => m);

            this.ViewBag.GenreId = new SelectList(genresQuery, "GenreId", "Name", selectedGenre);
        }
    }
}
