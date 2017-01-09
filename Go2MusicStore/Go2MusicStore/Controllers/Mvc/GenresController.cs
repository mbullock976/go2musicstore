namespace Go2MusicStore.Controllers.Mvc
{
    using System.Data.Entity.Infrastructure;
    using System.Net;
    using System.Web.Mvc;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Models;

    public class GenresController : BaseController
    {
        public GenresController(IApplicationManager applicationManager)
            :base(applicationManager)
        {
        }

        // GET: Genre
        public ActionResult Index()
        {
            var genres = this.AlbumManager.Get<Genre>();

            return this.View(genres);
        }

        // GET: Genre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genre genre = this.AlbumManager.GetById<Genre>(id);

            if (genre == null)
            {
                return this.HttpNotFound();
            }

            return this.View(genre);
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id, Name, Description")] Genre genre)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this.AlbumManager.Add<Genre>(genre);
                    this.AlbumManager.Save();                    
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                // Log the error (uncomment dex variable name and add a line here to write a log.)
                this.ModelState.AddModelError(
                    string.Empty,
                    "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return this.RedirectToAction("Index");
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genre genre = this.AlbumManager.GetById<Genre>(id);

            if (genre == null)
            {
                return this.HttpNotFound();
            }

            return this.View(genre);
        }

        // POST: Courses/Edit/5
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

            var genreToUpdate = this.AlbumManager.GetById<Genre>(id);

            if (this.TryUpdateModel(genreToUpdate, string.Empty, new[] { "Name", "Description" }))
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

            return this.View(genreToUpdate);
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genre genre = this.AlbumManager.GetById<Genre>(id);

            if (genre == null)
            {
                return this.HttpNotFound();
            }

            return this.View(genre);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genre genre = this.AlbumManager.GetById<Genre>(id);
            this.AlbumManager.Delete(genre);
            this.AlbumManager.Save();

            return this.RedirectToAction("Index");
        }
    }
}
