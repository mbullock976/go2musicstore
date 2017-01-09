using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Go2MusicStore.Models;
using Go2MusicStore.Platform.Implementation.DataLayer;

namespace Go2MusicStore.Controllers.Mvc
{
    using System.Data.Entity.Infrastructure;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;

    using NSubstitute.Routing.Handlers;

    public class CountriesController : BaseController
    {
        public CountriesController(IApplicationManager applicationManager)
            :base(applicationManager)
        {           
        }

        // GET: Countries
        public ActionResult Index()
        {
            var countries = this.StoreAccountManager.Get<Country>();
            return View(countries);
        }

        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = this.StoreAccountManager.GetById<Country>(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryId,Name")] Country country)
        {
            var countryExists = this.StoreAccountManager.Get<Country>().Any(m => m.Name == country.Name);
            if (countryExists)
            {
                ModelState.AddModelError(string.Empty, "Country already exists");
                return this.View(country);
            }

            if (ModelState.IsValid)
            {
                this.StoreAccountManager.Add(country);
                this.StoreAccountManager.Save();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = this.StoreAccountManager.GetById<Country>(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
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

            var countryToUpdate = this.StoreAccountManager.GetById<Country>(id);

            if (this.TryUpdateModel(countryToUpdate, string.Empty, new[] { "Name" }))
            {
                try
                {
                    this.StoreAccountManager.Save();

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

            return this.View(countryToUpdate);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = this.StoreAccountManager.GetById<Country>(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = this.StoreAccountManager.GetById<Country>(id);
            this.StoreAccountManager.Delete(country);
            this.StoreAccountManager.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.StoreAccountManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
