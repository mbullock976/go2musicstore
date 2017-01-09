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

    public class ShoppingCartsController : BaseController
    {
        public ShoppingCartsController(IApplicationManager applicationManager)
            : base(applicationManager)
        {            
        }

        // GET: ShoppingCarts
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

            IQueryable<ShoppingCart> shoppingCarts =
                this.StoreAccountManager.Get<ShoppingCart>(includeProperties: "StoreAccount, ShoppingCartItems")
                    .Where(c => !SelectedStoreAccount.HasValue
                    || c.StoreAccountId == SelectedStoreAccount)
                    .OrderBy(d => d.StoreAccountId)
                    .AsQueryable();

            return View(shoppingCarts.ToList());
        }

        // GET: ShoppingCarts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = this.StoreAccountManager.GetById<ShoppingCart>(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        public ActionResult Create()
        {
            PopulateStoreAccountsDropDownList();
            return View();
        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShoppingCartId,StoreAccountId")] ShoppingCart shoppingCart)
        {
            var shoppingCartExists =
                this.StoreAccountManager.Get<ShoppingCart>()
                    .Any(
                        m => m.StoreAccountId == shoppingCart.StoreAccountId);

            if (shoppingCartExists)
            {
                ModelState.AddModelError(string.Empty, "ShoppingCart already exists");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var storeAccount =
                    this.StoreAccountManager.Get<StoreAccount>()
                        .Single(m => m.StoreAccountId == shoppingCart.StoreAccountId);

                shoppingCart.StoreAccount = storeAccount;                

                this.StoreAccountManager.Add(shoppingCart);
                this.StoreAccountManager.Save();

                var shoppingCartSaved =
                    this.StoreAccountManager.Get<ShoppingCart>()
                        .Single(m => m.ShoppingCartId == shoppingCart.ShoppingCartId);

                storeAccount.ShoppingCartId = shoppingCartSaved.ShoppingCartId;
                this.StoreAccountManager.Save();

                return RedirectToAction("Index");
            }

            this.PopulateStoreAccountsDropDownList(shoppingCart.StoreAccountId);
            return View(shoppingCart);
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
