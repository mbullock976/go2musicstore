using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Go2MusicStore.Controllers.WebApi
{
    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.Models;

    public class ShoppingCartItemsApiController : BaseApiController
    {
        public ShoppingCartItemsApiController(IApplicationManager applicationManager)
           : base(applicationManager)
        {
        }

        // GET: api/ShoppingCartItemsApi
        [HttpGet]
        [Route("api/v1/shoppingCartItemsApi")]
        public IEnumerable<ShoppingCartItem> Get()
        {
            return this.StoreAccountManager.Get<ShoppingCartItem>();
        }

        // GET: api/ShoppingCartItemsApi/5
        [HttpGet]
        [Route("api/v1/shoppingCartItemsApi/{shoppingCartItemId}")]
        public ShoppingCartItem GetById(int shoppingCartItemId)
        {
            return this.StoreAccountManager.GetById<ShoppingCartItem>(shoppingCartItemId);
        }

        [HttpGet]
        [Route("api/v1/shoppingCartItemsApi/GetByShoppingCartId/{shoppingCartId?}")]
        public IEnumerable<ShoppingCartItem> GetByShoppingCartId(int? shoppingCartId)
        {
            return this.StoreAccountManager.Get<ShoppingCartItem>()
                .Where(m => m.ShoppingCartId == shoppingCartId);            
        }

        [HttpGet]
        [Route("api/v1/shoppingCartItemsApi/GetByShoppingCartAndAlbum/{albumId?}/{shoppingCartId?}")]
        public ShoppingCartItem GetByShoppingCartAndAlbum(int? albumId, int? shoppingCartId)
        {
            return this.StoreAccountManager.Get<ShoppingCartItem>()
                .FirstOrDefault(m => m.ShoppingCartId == shoppingCartId
                && m.AlbumId == albumId);
        }

        // POST: api/ShoppingCartItemsApi
        [HttpPost]
        [Route("api/v1/shoppingCartItemsApi")]
        public HttpResponseMessage Post([FromBody]ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem.ShoppingCartId == 0 || shoppingCartItem.AlbumId == 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, shoppingCartItem);
            }

            var shoppingCartItemFound =
                this.StoreAccountManager.Get<ShoppingCartItem>()
                    .FirstOrDefault(
                        m => m.ShoppingCartId == shoppingCartItem.ShoppingCartId 
                        && m.AlbumId == shoppingCartItem.AlbumId);

            if (shoppingCartItemFound != null)
            {
                shoppingCartItemFound.Quantity += shoppingCartItem.Quantity;
                this.StoreAccountManager.Save();
                return this.Request.CreateResponse(HttpStatusCode.Accepted, shoppingCartItem);
            }

            try
            {
                this.StoreAccountManager.Add(shoppingCartItem);
                this.StoreAccountManager.Save();

                return this.Request.CreateResponse(HttpStatusCode.Created, shoppingCartItem);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, shoppingCartItem);
            }
        }
        
        // PUT: api/ShoppingCartItemsApi/5
        [HttpPut]
        [Route("api/v1/shoppingCartItemsApi")]
        public HttpResponseMessage Put([FromBody] ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem.ShoppingCartItemId == 0 
                || shoppingCartItem.ShoppingCartId == 0
                || shoppingCartItem.AlbumId == 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, shoppingCartItem);
            }

            try
            {
                var shoppingCartItemToUpdate = this.StoreAccountManager.GetById<ShoppingCartItem>(shoppingCartItem.ShoppingCartItemId);

                shoppingCartItemToUpdate.Quantity = shoppingCartItem.Quantity;

                this.StoreAccountManager.Save();

                return this.Request.CreateResponse(HttpStatusCode.Accepted, shoppingCartItemToUpdate);
            }
            catch (Exception)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, shoppingCartItem);
            }
        }

        // DELETE: api/ShoppingCartItemsApi/5
        [HttpDelete]
        [Route("api/v1/shoppingCartItemsApi/{id}")]
        public void Delete([FromUri]int? id)
        {
            var shoppingCartItem = this.StoreAccountManager.GetById<ShoppingCartItem>(id);
            if (shoppingCartItem != null)
            {
                this.StoreAccountManager.Delete(shoppingCartItem);
                this.StoreAccountManager.Save();
            }
        }

        // DELETE: api/ShoppingCartItemsApi/5
        [HttpDelete]
        [Route("api/v1/shoppingCartItemsApi/DeleteByShoppingCartId/{shoppingCartId?}")]
        public void DeleteByShoppingCartId([FromUri]int? shoppingCartId)
        {
            var shoppingCartItems =
                this.StoreAccountManager.Get<ShoppingCartItem>().Where(m => m.ShoppingCartId == shoppingCartId);

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                this.StoreAccountManager.Delete(shoppingCartItem);                
            }

            this.StoreAccountManager.Save();
        }
    }
}
