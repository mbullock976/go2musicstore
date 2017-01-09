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

    public class CreditCardTypesController : BaseController
    {

        public CreditCardTypesController(IApplicationManager applicationManager)
            :base(applicationManager)
        {
        }

        // GET: CreditCardTypes
        public ActionResult Index()
        {
            return this.View(this.StoreAccountManager.Get<CreditCardType>());
        }

        // GET: CreditCardTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCardType creditCardType = this.StoreAccountManager.GetById<CreditCardType>(id);
            if (creditCardType == null)
            {
                return HttpNotFound();
            }
            return View(creditCardType);
        }

        // GET: CreditCardTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreditCardTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreditCardTypeId,Name")] CreditCardType creditCardType)
        {
            if (ModelState.IsValid)
            {
                this.StoreAccountManager.Add(creditCardType);
                this.StoreAccountManager.Save();
                return RedirectToAction("Index");
            }

            return View(creditCardType);
        }

        // GET: CreditCardTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCardType creditCardType = this.StoreAccountManager.GetById<CreditCardType>(id);
            if (creditCardType == null)
            {
                return HttpNotFound();
            }
            return View(creditCardType);
        }

        // POST: CreditCardTypes/Edit/5
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

            var creditCardTypeToUpdate = this.StoreAccountManager.GetById<CreditCardType>(id);

            if (this.TryUpdateModel(creditCardTypeToUpdate, string.Empty, new[] { "Name" }))
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
            
            return this.View(creditCardTypeToUpdate);
        }

        // GET: CreditCardTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCardType creditCardType = this.StoreAccountManager.GetById<CreditCardType>(id);
            if (creditCardType == null)
            {
                return HttpNotFound();
            }
            return View(creditCardType);
        }

        // POST: CreditCardTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditCardType creditCardType = this.StoreAccountManager.GetById<CreditCardType>(id);
            this.StoreAccountManager.Delete(creditCardType);
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
