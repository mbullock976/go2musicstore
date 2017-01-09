namespace Go2MusicStore.Controllers.Mvc
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Models;

    public class ReviewsController : BaseController
    {
        public ReviewsController(IApplicationManager applicationManager)
            :base(applicationManager)
        {
        }

        // GET: Reviews
        public ActionResult Index(int? SelectedAlbum)
        {
            var albums = this.AlbumManager
                .Get<Album>(orderBy: q => q.OrderBy(d => d.ReleaseDate)).ToList();

            this.ViewBag.SelectedAlbum = new SelectList(albums, "AlbumId", "Title", SelectedAlbum);
            int albumId = SelectedAlbum.GetValueOrDefault();

            IQueryable<Review> reviews =
                this.AlbumManager.Get<Review>(includeProperties: "Album")
                    .Where(c => !SelectedAlbum.HasValue || c.AlbumId == albumId)
                    .OrderBy(d => d.AlbumId)
                    .AsQueryable();

            //put breakpoint here to see the raw sql sent to database from EF
            //var sql = artists.ToString();

            return this.View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Review review = this.AlbumManager.GetById<Review>(id);

            if (review == null)
            {
                return this.HttpNotFound();
            }

            return this.View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            this.PopulateAlbumDropDownList();
            return this.View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewId, Title, Comment, StarRating, IsRecommended, AlbumId")] Review review)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    review.ReviewDate = DateTime.UtcNow;
                    this.AlbumManager.Add<Review>(review);
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

            this.PopulateAlbumDropDownList(review.AlbumId);
            return this.View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Review review = this.AlbumManager.GetById<Review>(id);

            if (review == null)
            {
                return this.HttpNotFound();
            }

            this.PopulateAlbumDropDownList(review.AlbumId);
            return this.View(review);
        }

        // POST: Reviews/Edit/5
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

            var reviewToUpdate = this.AlbumManager.GetById<Review>(id);

            if (this.TryUpdateModel(reviewToUpdate, string.Empty, new[] { "ReviewId", "Title", "Comment", "StarRating", "IsRecommended", "AlbumId" }))
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

            this.PopulateAlbumDropDownList(reviewToUpdate.AlbumId);
            return this.View(reviewToUpdate);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Review review = this.AlbumManager.GetById<Review>(id);

            if (review == null)
            {
                return this.HttpNotFound();
            }

            return this.View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = this.AlbumManager.GetById<Review>(id);
            this.AlbumManager.Delete(review);
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

        private void PopulateAlbumDropDownList(object selectedAlbum = null)
        {
            var albumsQuery =
               this.AlbumManager
               .Get<Album>(orderBy: m => m.OrderBy(p => p.ReleaseDate))
               .Select(m => m);

            this.ViewBag.AlbumId = new SelectList(albumsQuery, "AlbumId", "Title", selectedAlbum);
        }
    }
}
