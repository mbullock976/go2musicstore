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
    using Go2MusicStore.Common;

    public class PurchaseOrdersController : BaseController
    {
        public PurchaseOrdersController(IApplicationManager applicationManager)
                   : base(applicationManager)
        {
        }

        // GET: PurchaseOrders
        public ActionResult Index(int? SelectedStoreAccount)
        {
            if (SelectedStoreAccount.HasValue)
            {
                PopulateStoreAccountsDropDownList(SelectedStoreAccount);
            }
            else
            {
                PopulateStoreAccountsDropDownList();
            }

            IQueryable<PurchaseOrder> purchaseOrders =
               this.StoreAccountManager.Get<PurchaseOrder>(includeProperties: "StoreAccount, PurchaseOrderItems")
                   .Where(c => !SelectedStoreAccount.HasValue
                   || c.StoreAccountId == SelectedStoreAccount)
                   .OrderBy(d => d.PurchaseOrderId)
                   .CalculateTotalOrderAmount()
                   .AsQueryable();

            return View(purchaseOrders.ToList());
        }

        // GET: PurchaseOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = this.StoreAccountManager.GetById<PurchaseOrder>(id).CalculateTotalOrderAmount();
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Create
        public ActionResult Create()
        {
            PopulateStoreAccountsDropDownList();
            return View();
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseOrderId,StoreAccountId,PurchaseDate,TotalOrderAmount")] PurchaseOrder purchaseOrder)
        {
            var purchaseOrderExists =
                this.StoreAccountManager.Get<PurchaseOrder>()
                    .Any(
                        m => m.StoreAccountId == purchaseOrder.StoreAccountId);

            if (purchaseOrderExists)
            {
                ModelState.AddModelError(string.Empty, "Purchase order already exists");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var storeAccount =
                    this.StoreAccountManager.Get<StoreAccount>()
                        .Single(m => m.StoreAccountId == purchaseOrder.StoreAccountId);

                purchaseOrder.StoreAccount = storeAccount;
                purchaseOrder.CalculateTotalOrderAmount();

                this.StoreAccountManager.Add(purchaseOrder);
                this.StoreAccountManager.Save();

                return RedirectToAction("Index");
            }

            this.PopulateStoreAccountsDropDownList(purchaseOrder.StoreAccountId);
            return View(purchaseOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.StoreAccountManager.Dispose();
            }
            base.Dispose(disposing);
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
