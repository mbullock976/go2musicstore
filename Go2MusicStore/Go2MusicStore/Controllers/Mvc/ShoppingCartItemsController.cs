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

    public class ShoppingCartItemsController : BaseController
    {
        public ShoppingCartItemsController(IApplicationManager applicationManager)
            : base(applicationManager)
        {            
        }

        // GET: ShoppingCartItems
        public ActionResult Index(int? SelectedShoppingCart)
        {
            if (SelectedShoppingCart.HasValue)
            {
                PopulateShoppingCartsDropdownList(SelectedShoppingCart);
            }
            else
            {
                PopulateShoppingCartsDropdownList();
            }

            IQueryable<ShoppingCartItem> shoppingCartItems =
                this.StoreAccountManager.Get<ShoppingCartItem>(includeProperties: "Album, ShoppingCart")
                    .Where(c => !SelectedShoppingCart.HasValue
                    || c.ShoppingCartId == SelectedShoppingCart)
                    .OrderBy(d => d.ShoppingCartItemId)
                    .AsQueryable();
            
            return View(shoppingCartItems.ToList());
        }

        // GET: ShoppingCartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartItem shoppingCartItem = this.StoreAccountManager.GetById<ShoppingCartItem>(id);
            if (shoppingCartItem == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Create
        public ActionResult Create()
        {
            this.PopulateShoppingCartsDropdownList();
            PopulateAlbumDropdownList();
            return View();
        }        

        // POST: ShoppingCartItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShoppingCartId,AlbumId,Quantity")] ShoppingCartItem shoppingCartItem)
        {
            var shoppingCartItemExists =
                this.StoreAccountManager.Get<ShoppingCartItem>()
                    .Any(
                        m => m.AlbumId == shoppingCartItem.AlbumId);

            if (shoppingCartItemExists)
            {
                ModelState.AddModelError(string.Empty, "ShoppingCartItem already exists");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                this.StoreAccountManager.Add(shoppingCartItem);
                this.StoreAccountManager.Save();
                return RedirectToAction("Index");
            }

            this.PopulateShoppingCartsDropdownList(shoppingCartItem.ShoppingCartId);
            this.PopulateAlbumDropdownList(shoppingCartItem.AlbumId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartItem shoppingCartItem = this.StoreAccountManager.GetById<ShoppingCartItem>(id);
            if (shoppingCartItem == null)
            {
                return HttpNotFound();
            }

            this.PopulateShoppingCartsDropdownList(shoppingCartItem.ShoppingCartId);
            this.PopulateAlbumDropdownList(shoppingCartItem.AlbumId);
            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShoppingCartItemId,ShoppingCartId,AlbumId,Quantity,ShoppingCart")] ShoppingCartItem shoppingCartItem)
        {
            if (ModelState.IsValid)
            {
                var shoppingCartItemToUpdate = this.StoreAccountManager.GetById<ShoppingCartItem>(shoppingCartItem.ShoppingCartItemId);

                if (this.TryUpdateModel(shoppingCartItemToUpdate, string.Empty, new[] { "AlbumId", "Quantity" }))
                {
                    this.StoreAccountManager.Save();
                    return RedirectToAction("Index");
                }
            }
            this.PopulateShoppingCartsDropdownList(shoppingCartItem.ShoppingCartId);
            this.PopulateAlbumDropdownList(shoppingCartItem.AlbumId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartItem shoppingCartItem = this.StoreAccountManager.GetById<ShoppingCartItem>(id);
            if (shoppingCartItem == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingCartItem shoppingCartItem = this.StoreAccountManager.GetById<ShoppingCartItem>(id);
            this.StoreAccountManager.Delete(shoppingCartItem);
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

        private void PopulateAlbumDropdownList(object selectedAlbumId = null)
        {
            var albumsQuery =
                 this.AlbumManager.Get<Album>()
                 .OrderBy(m => m.Title)
                 .Select(m => m);

            this.ViewBag.AlbumId = new SelectList(albumsQuery, "AlbumId", "Title", selectedAlbumId);
        }

        private void PopulateShoppingCartsDropdownList(object selectedShoppingCartId = null)
        {
            var shoppingCartsQuery =
                 this.StoreAccountManager.Get<ShoppingCart>()
                 .OrderBy(m => m.ShoppingCartId)
                 .Select(m => m);

            this.ViewBag.ShoppingCartId = new SelectList(shoppingCartsQuery, "ShoppingCartId", "StoreAccount.UserIdentityName", selectedShoppingCartId);
        }
    }
}
