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

    public class StoreAccountsController : BaseController
    {

        public StoreAccountsController(IApplicationManager applicationManager)
            :base(applicationManager)
        {            
        }

        // GET: StoreAccounts
        public ActionResult Index()
        {
            var storeAccounts = this.StoreAccountManager
                .Get<StoreAccount>(includeProperties: "CreditCards, Country, ShoppingCart");
            return this.View(storeAccounts);
        }

        // GET: StoreAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreAccount storeAccount = this.StoreAccountManager.Get<StoreAccount>().Single(m => m.StoreAccountId == id);
            if (storeAccount == null)
            {
                return HttpNotFound();
            }
            return View(storeAccount);
        }

        // GET: StoreAccounts/Create
        public ActionResult Create()
        {
            this.PopulateUsersDropDownList();
            this.PopulateCountriesDropDownList();
            return View();
        }

        // POST: StoreAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserIdentityName,FirstName,LastName,TelephoneNo,Address,City,PostCode,CountryId,EmailAddress")] StoreAccount storeAccount)
        {
            try
            {
                var storeAccountExists =
                this.StoreAccountManager.Get<StoreAccount>()
                    .Any(
                        m => m.UserIdentityName == storeAccount.UserIdentityName);

                if (storeAccountExists)
                {
                    ModelState.AddModelError(string.Empty, "StoreAccount already exists");
                    return RedirectToAction("Index");
                }

                var storeAccountExist =
                    this.StoreAccountManager.Get<StoreAccount>()
                        .FirstOrDefault(m => m.UserIdentityName == storeAccount.UserIdentityName);

                if (storeAccountExist != null)
                {
                    this.ModelState.AddModelError(
                    string.Empty,
                    "Store Account for " + storeAccountExist.UserIdentityName + " already exists.");
                }

                if (ModelState.IsValid)
                {
                    this.StoreAccountManager.Add(storeAccount);
                    this.StoreAccountManager.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                // Log the error (uncomment dex variable name and add a line here to write a log.)
                this.ModelState.AddModelError(
                    string.Empty,
                    "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            this.PopulateCountriesDropDownList(storeAccount.CountryId);
            this.PopulateUsersDropDownList(storeAccount.UserIdentityName);
            return View(storeAccount);
        }

        // GET: StoreAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            StoreAccount storeAccount = this.StoreAccountManager.Get<StoreAccount>().Single(m => m.StoreAccountId == id);

            if (storeAccount == null)
            {
                return HttpNotFound();
            }
            this.PopulateCountriesDropDownList(storeAccount.CountryId);
            this.PopulateUsersDropDownList(storeAccount.UserIdentityName);
            return View(storeAccount);
        }

        // POST: StoreAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserIdentityName,FirstName,LastName,TelephoneNo,Address,City,PostCode,CountryId,EmailAddress,ShoppingCartId, StoreAccountId")] StoreAccount storeAccount)
        {
            if (ModelState.IsValid)
            {
                var storeAccountToUpdate = this.StoreAccountManager.Get<StoreAccount>().Single(m => m.StoreAccountId == storeAccount.StoreAccountId);

                if (this.TryUpdateModel(storeAccountToUpdate, string.Empty, new [] { "FirstName", "LastName", "TelephoneNo", "Address", "City", "PostCode", "CountryId", "EmailAddress" }))
                {
                    this.StoreAccountManager.Save();
                    return RedirectToAction("Index");
                }
            }
            this.PopulateCountriesDropDownList(storeAccount.CountryId);
            this.PopulateUsersDropDownList(storeAccount.UserIdentityName);
            return View(storeAccount);
        }

        // GET: StoreAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreAccount storeAccount = this.StoreAccountManager.Get<StoreAccount>().Single(m => m.StoreAccountId == id);
            if (storeAccount == null)
            {
                return HttpNotFound();
            }
            return View(storeAccount);
        }

        // POST: StoreAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreAccount storeAccount = this.StoreAccountManager.Get<StoreAccount>().Single(m => m.StoreAccountId == id);
            if (storeAccount.CreditCards.Any())
            {
                ModelState.AddModelError(string.Empty, "Credit Cards exist. Delete these first");
                return this.View(storeAccount);
            }
            
            this.StoreAccountManager.Delete(storeAccount);
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

        private void PopulateCountriesDropDownList(object selectedCountry = null)
        {
            var countriesQuery =
                this.AlbumManager
                .Get<Country>(orderBy: m => m.OrderBy(p => p.Name))
                .Select(m => m);

            this.ViewBag.CountryId = new SelectList(countriesQuery, "CountryId", "Name", selectedCountry);
        }

        private void PopulateUsersDropDownList(object selectedUser = null)
        {
            var usersQuery =
                this.SecurityManager.ApplicationUsers
                .OrderBy(m => m.UserName)
                .Select(m => m);

            this.ViewBag.UserIdentityName = new SelectList(usersQuery, "UserName", "UserName", selectedUser);
        }
    }
}
