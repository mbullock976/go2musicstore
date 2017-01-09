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
    using Go2MusicStore.API.Interfaces;

    public class CreditCardsController : BaseController
    {
        public CreditCardsController(IApplicationManager applicationManager)
            : base(applicationManager)
        {            
        }

        // GET: CreditCards
        public ActionResult Index(int? SelectedStoreAccount)
        {
            if (SelectedStoreAccount.HasValue)
            {
                var storeAccount = this.StoreAccountManager.GetById<StoreAccount>(SelectedStoreAccount);
                        
                PopulateStoreAccountsDropDownList(storeAccount.StoreAccountId);
            }
            else
            {
                PopulateStoreAccountsDropDownList();
            }

            IQueryable<CreditCard> creditCards =
                this.AlbumManager.Get<CreditCard>(includeProperties: "StoreAccount")
                    .Where(c => !SelectedStoreAccount.HasValue || c.StoreAccountId == SelectedStoreAccount)
                    .OrderBy(d => d.CreditCardId)
                    .AsQueryable();

            //put breakpoint here to see the raw sql sent to database from EF
            //var sql = artists.ToString();

            return this.View(creditCards.ToList());

        }

        // GET: CreditCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = this.StoreAccountManager.GetById<CreditCard>(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // GET: CreditCards/Create
        public ActionResult Create()
        {
            PopulateStoreAccountsDropDownList();
            PopulateCreditCardTypesDropDownList();
            return View();
        }

        // POST: CreditCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreditCardId,CreditCardTypeId,CardNumber,StoreAccountId")] CreditCard creditCard)
        {
            var creditCardExists =
                this.StoreAccountManager.Get<CreditCard>()
                    .Any(
                        m => m.CardNumber == creditCard.CardNumber 
                        &&  m.CreditCardTypeId == creditCard.CreditCardTypeId);

            if (creditCardExists)
            {
                ModelState.AddModelError(string.Empty, "CreditCard already exists");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var storeAccount =
                    this.StoreAccountManager.Get<StoreAccount>()
                        .Single(m => m.StoreAccountId == creditCard.StoreAccountId);

                creditCard.StoreAccount = storeAccount;                

                this.StoreAccountManager.Add(creditCard);
                this.StoreAccountManager.Save();
                return RedirectToAction("Index");
            }

            this.PopulateStoreAccountsDropDownList(creditCard.StoreAccountId);
            PopulateCreditCardTypesDropDownList(creditCard.CreditCardTypeId);
            return View(creditCard);
        }

        // GET: CreditCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = this.StoreAccountManager.GetById<CreditCard>(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            PopulateCreditCardTypesDropDownList(creditCard.CreditCardTypeId);
            return View(creditCard);
        }

        // POST: CreditCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CreditCardId,CardNumber,CreditCardTypeId,StoreAccount")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                var creditCardToUpdate = this.StoreAccountManager.GetById<CreditCard>(creditCard.CreditCardId);

                if (this.TryUpdateModel(creditCardToUpdate, string.Empty, new[] { "CardNumber", "CreditCardTypeId" }))
                {
                    this.StoreAccountManager.Save();
                    return RedirectToAction("Index");
                }
            }
            this.PopulateCreditCardTypesDropDownList(creditCard.CreditCardTypeId);
            return View(creditCard);
        }

        // GET: CreditCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = this.StoreAccountManager.GetById<CreditCard>(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditCard creditCard = this.StoreAccountManager.GetById<CreditCard>(id);
            this.StoreAccountManager.Delete(creditCard);
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

        private void PopulateCreditCardTypesDropDownList(object selectedCreditCardType = null)
        {
            var creditCardTypesQuery =
                this.StoreAccountManager
                .Get<CreditCardType>(orderBy: m => m.OrderBy(p => p.Name))
                .Select(m => m);

            this.ViewBag.CreditCardTypeId = new SelectList(creditCardTypesQuery, "CreditCardTypeId", "Name", selectedCreditCardType);
        }
        private void PopulateStoreAccountsDropDownList(object selectedStoreAccountId = null)
        {
            var storeAccountsQuery =
                this.StoreAccountManager.Get<StoreAccount>()
                .OrderBy(m => m.StoreAccountId)
                .Select(m => m);

            this.ViewBag.StoreAccountId = new SelectList(storeAccountsQuery, "StoreAccountId", "UserIdentityName", selectedStoreAccountId);
        }
    }
}
