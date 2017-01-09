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

    public class PurchaseOrderItemsController : BaseController
    {
        public PurchaseOrderItemsController(IApplicationManager applicationManager)
            : base(applicationManager)
        {
        }

        // GET: PurchaseOrderItems
        public ActionResult Index(int? SelectedPurchaseOrder)
        {
            if (SelectedPurchaseOrder.HasValue)
            {
                PopulatePurchaseOrdersDropdownList(SelectedPurchaseOrder);
            }
            else
            {
                PopulatePurchaseOrdersDropdownList();
            }

            IQueryable<PurchaseOrderItem> purchaseOrderItems =
                this.StoreAccountManager.Get<PurchaseOrderItem>(includeProperties: "Album, PurchaseOrder")
                    .Where(c => !SelectedPurchaseOrder.HasValue
                    || c.PurchaseOrderId == SelectedPurchaseOrder)
                    .OrderBy(d => d.PurchaseOrderItemId)
                    .CalculateTotalOrderItemAmount()
                    .AsQueryable();

            return View(purchaseOrderItems.ToList());
        }

        // GET: PurchaseOrderItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderItem purchaseOrderItem = this.StoreAccountManager.GetById<PurchaseOrderItem>(id).CalculateTotalOrderItemAmount();
            if (purchaseOrderItem == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderItem);
        }

        // GET: PurchaseOrderItems/Create
        public ActionResult Create()
        {
            this.PopulatePurchaseOrdersDropdownList();
            PopulateAlbumDropdownList();
            return View();
        }

        // POST: PurchaseOrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseOrderId,AlbumId,Quantity,TotalAmount")] PurchaseOrderItem purchaseOrderItem)
        {
            var purchaseOrderItemExists =
                this.StoreAccountManager.Get<PurchaseOrderItem>()
                    .Any(
                        m => m.AlbumId == purchaseOrderItem.AlbumId);

            if (purchaseOrderItemExists)
            {
                ModelState.AddModelError(string.Empty, "purchaseOrderItem already exists");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                purchaseOrderItem.CalculateTotalOrderItemAmount();
                this.StoreAccountManager.Add(purchaseOrderItem);
                this.StoreAccountManager.Save();
                return RedirectToAction("Index");
            }

            this.PopulatePurchaseOrdersDropdownList(purchaseOrderItem.PurchaseOrderId);
            this.PopulateAlbumDropdownList(purchaseOrderItem.AlbumId);
            return View(purchaseOrderItem);
        }

        // GET: PurchaseOrderItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderItem purchaseOrderItem = this.StoreAccountManager.GetById<PurchaseOrderItem>(id).CalculateTotalOrderItemAmount();
            if (purchaseOrderItem == null)
            {
                return HttpNotFound();
            }
            
            this.PopulatePurchaseOrdersDropdownList(purchaseOrderItem.PurchaseOrderId);
            this.PopulateAlbumDropdownList(purchaseOrderItem.AlbumId);
            return View(purchaseOrderItem);
        }

        // POST: PurchaseOrderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseOrderItemId,PurchaseOrderId,AlbumId,Quantity,TotalAmount")] PurchaseOrderItem purchaseOrderItem)
        {
            if (ModelState.IsValid)
            {
                var purchaseOrderItemToUpdate = this.StoreAccountManager.GetById<PurchaseOrderItem>(purchaseOrderItem.PurchaseOrderItemId);

                if (this.TryUpdateModel(purchaseOrderItemToUpdate, string.Empty, new[] { "AlbumId", "Quantity", "TotalAmount" }))
                {
                    purchaseOrderItemToUpdate.CalculateTotalOrderItemAmount();
                    this.StoreAccountManager.Save();
                    return RedirectToAction("Index");
                }
            }
            this.PopulatePurchaseOrdersDropdownList(purchaseOrderItem.PurchaseOrderId);
            this.PopulateAlbumDropdownList(purchaseOrderItem.AlbumId);
            return View(purchaseOrderItem);
        }

        // GET: PurchaseOrderItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderItem purchaseOrderItem = this.StoreAccountManager.GetById<PurchaseOrderItem>(id);
            if (purchaseOrderItem == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderItem);
        }

        // POST: PurchaseOrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrderItem purchaseOrderItem = this.StoreAccountManager.GetById<PurchaseOrderItem>(id).CalculateTotalOrderItemAmount();
            this.StoreAccountManager.Delete(purchaseOrderItem);
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

        private void PopulatePurchaseOrdersDropdownList(int? selectedPurchaseOrder = null)
        {
            var purchaseOrdersQuery =
                 this.StoreAccountManager.Get<PurchaseOrder>()
                 .OrderBy(m => m.PurchaseOrderId)
                 .Select(m => m);

            this.ViewBag.PurchaseOrderId = new SelectList(purchaseOrdersQuery, "PurchaseOrderId", "StoreAccount.UserIdentityName", selectedPurchaseOrder);
        }

        private void PopulateAlbumDropdownList(object selectedAlbumId = null)
        {
            var albumsQuery =
                 this.AlbumManager.Get<Album>()
                 .OrderBy(m => m.Title)
                 .Select(m => m);

            this.ViewBag.AlbumId = new SelectList(albumsQuery, "AlbumId", "Title", selectedAlbumId);
        }

    }
}
