using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Go2MusicStore.Controllers.WebApi
{
    using System.Web;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Models;

    public class StoreAccountsApiController : BaseApiController
    {
        public StoreAccountsApiController(IApplicationManager applicationManager)
            : base(applicationManager)
        {

        }

        [HttpGet]
        [Route("api/v1/StoreAccountsApi/")]
        public StoreAccount Get()
        {
            //try to get based on who is logged in
            var userIdentityName = HttpContext.Current.User.Identity.Name;
            var storeAccount =
                this.StoreAccountManager.Get<StoreAccount>().FirstOrDefault(m => m.UserIdentityName == userIdentityName);
            if (storeAccount == null)
            {
                storeAccount = new StoreAccount();
            }

            return storeAccount;
        }

        [HttpGet]
        [Route("api/v1/StoreAccountsApi/{userIdentityName?}")]
        public StoreAccount Get([FromUri] string userIdentityName)
        {
            var storeAccount =
                this.StoreAccountManager.Get<StoreAccount>().FirstOrDefault(m => m.UserIdentityName == userIdentityName);
            if (storeAccount == null)
            {
                return new StoreAccount();
            }

            return storeAccount;
        }

        [HttpPost]
        [Route("api/v1/StoreAccountsApi")]
        public HttpResponseMessage Post([FromBody] StoreAccount newStoreAccount)
        {
            if (string.IsNullOrEmpty(newStoreAccount.UserIdentityName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, newStoreAccount);
            }

            var storeAccountExists =
               this.StoreAccountManager.Get<StoreAccount>()
                   .Any(
                       m => m.UserIdentityName == newStoreAccount.UserIdentityName);

            if (storeAccountExists)
            {
                return this.Request.CreateResponse(
                    HttpStatusCode.BadRequest, newStoreAccount, "store account already exists");
            }

            try
            {
                this.StoreAccountManager.Add<StoreAccount>(newStoreAccount);
                this.StoreAccountManager.Save();


                var shoppingCartExists =
                   this.StoreAccountManager.Get<ShoppingCart>()
                       .Any(
                           m => m.StoreAccountId == newStoreAccount.StoreAccountId);

                if (shoppingCartExists)
                {
                    return this.Request.CreateResponse(
                        HttpStatusCode.BadRequest, newStoreAccount, "shopping cart already exists");
                }


                //create and save shopping cart 
                var newShoppingCart = new ShoppingCart
                                          {
                                              StoreAccountId = newStoreAccount.StoreAccountId,
                                              StoreAccount = newStoreAccount
                                          };

                this.StoreAccountManager.Add(newShoppingCart);
                this.StoreAccountManager.Save();

                //then pass the newly created shoppingcartid to new store account
                newStoreAccount.ShoppingCartId = newShoppingCart.ShoppingCartId;
                this.StoreAccountManager.Save();

            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, newStoreAccount);
            }

            return Request.CreateResponse(HttpStatusCode.Created, newStoreAccount);
        }

        [HttpPut]
        [Route("api/v1/StoreAccountsApi")]
        public HttpResponseMessage Put([FromBody] StoreAccount storeAccount)
        {
            if (storeAccount == null || storeAccount.StoreAccountId == 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, storeAccount);
            }

            try
            {
                var storeAccountToUpdate =
                    this.StoreAccountManager.GetById<StoreAccount>(
                        new object[] { storeAccount.StoreAccountId, storeAccount.UserIdentityName });

                storeAccountToUpdate.FirstName = storeAccount.FirstName;
                storeAccountToUpdate.LastName = storeAccount.LastName;
                storeAccountToUpdate.TelephoneNo = storeAccount.TelephoneNo;
                storeAccountToUpdate.Address = storeAccount.Address;
                storeAccountToUpdate.CountryId = storeAccount.CountryId;
                storeAccountToUpdate.City = storeAccount.City;
                storeAccountToUpdate.PostCode = storeAccount.PostCode;
                storeAccountToUpdate.EmailAddress = storeAccount.EmailAddress;

                this.StoreAccountManager.Save();

                return this.Request.CreateResponse(HttpStatusCode.OK, storeAccountToUpdate);
            }
            catch (Exception)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, storeAccount);
            }           
        }
    }
}
