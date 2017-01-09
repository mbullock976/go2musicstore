namespace Go2MusicStore.Controllers.Mvc
{
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Models;

    public class AlbumsController : BaseController
    {
        public AlbumsController(IApplicationManager applicationManager)
            :base(applicationManager)
        {
        }

        // GET: Albums
        public ActionResult Index(int? SelectedArtist)
        {
            var artists = this.AlbumManager
                .Get<Artist>(orderBy: q => q.OrderBy(d => d.Name)).ToList();

            this.ViewBag.SelectedArtist = new SelectList(artists, "ArtistId", "Name", SelectedArtist);
            int artistId = SelectedArtist.GetValueOrDefault();

            IQueryable<Album> albums =
                this.AlbumManager.Get<Album>(includeProperties: "Artist, Reviews")
                    .Where(c => !SelectedArtist.HasValue || c.ArtistId == artistId)
                    .OrderBy(d => d.AlbumId)
                    .AsQueryable();

            //put breakpoint here to see the raw sql sent to database from EF
            //var sql = artists.ToString();

            return this.View(albums.ToList());
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album album = this.AlbumManager.GetById<Album>(id);

            if (album == null)
            {
                return this.HttpNotFound();
            }

            return this.View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            this.PopulateArtistDropDownList();
            return this.View();
        }
               
        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId, Title, Description, ReleaseDate, Price, StockCount, TotalStarRating, ArtistId")] Album album)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    album.AlbumArtUrl = "/Content/Images/placeholder.gif";
                    this.AlbumManager.Add<Album>(album);
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

            this.PopulateArtistDropDownList(album.ArtistId);
            return this.View(album);
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album album = this.AlbumManager.GetById<Album>(id);

            if (album == null)
            {
                return this.HttpNotFound();
            }

            this.PopulateArtistDropDownList(album.ArtistId);
            return this.View(album);
        }


        // POST: Albums/Edit/5
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

            var albumToUpdate = this.AlbumManager.GetById<Album>(id);

            if (this.TryUpdateModel(albumToUpdate, string.Empty, new[] { "Title", "Description", "ReleaseDate", "Price", "StockCount", "TotalStarRating", "ArtistId" }))
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

            this.PopulateArtistDropDownList(albumToUpdate.ArtistId);
            return this.View(albumToUpdate);
        }

        // GET: Album/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album album = this.AlbumManager.GetById<Album>(id);

            if (album == null)
            {
                return this.HttpNotFound();
            }

            return this.View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = this.AlbumManager.GetById<Album>(id);
            this.AlbumManager.Delete(album);
            this.AlbumManager.Save();

            return this.RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateArtistDropDownList(object selectedArtist = null)
        {
            var artistsQuery =
                this.AlbumManager
                .Get<Artist>(orderBy: m => m.OrderBy(p => p.Name))
                .Select(m => m);

            this.ViewBag.ArtistId = new SelectList(artistsQuery, "ArtistId", "Name", selectedArtist);
        }

    }
}
