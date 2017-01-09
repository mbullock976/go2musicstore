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

    public class CreditCardsApiController : BaseApiController
    {
        public CreditCardsApiController(IApplicationManager applicationManager)
            : base(applicationManager)
        {
        }

        [HttpGet]
        [Route("api/v1/CreditCardsApi/GetByStoreAccount/{userIdentityName?}")]
        public IEnumerable<CreditCard> GetByStoreAccount([FromUri] string userIdentityName)
        {
            var storeAccount = this.StoreAccountManager.Get<StoreAccount>()
                .FirstOrDefault(m => m.UserIdentityName == userIdentityName);

            if (storeAccount == null)
            {
                return new List<CreditCard>();
            }

            var creditCard = this.StoreAccountManager.Get<CreditCard>().Where(m => m.StoreAccountId == storeAccount.StoreAccountId);
            return creditCard;
        }

        // GET: api/CreditCardsApi
        [HttpGet]
        [Route("api/v1/CreditCardsApi")]
        public IEnumerable<CreditCard> Get()
        {
            return this.StoreAccountManager.Get<CreditCard>();
        }

        // GET: api/CreditCardsApi/5
        [HttpGet]
        [Route("api/v1/CreditCardsApi/{id}")]
        public CreditCard Get([FromUri] int id)
        {
            return this.StoreAccountManager.GetById<CreditCard>(id);
        }

        // POST: api/CreditCardsApi
        [HttpPost]
        [Route("api/v1/CreditCardsApi")]
        public HttpResponseMessage Post([FromBody] CreditCard creditCard)
        {
            if (creditCard.CreditCardTypeId == 0 || string.IsNullOrEmpty(creditCard.CardNumber))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, creditCard);
            }

            var creditCardExists =
                this.StoreAccountManager.Get<CreditCard>()
                    .Any(
                        m => m.CardNumber == creditCard.CardNumber && m.CreditCardTypeId == creditCard.CreditCardTypeId);

            if (creditCardExists)
            {
                return this.Request.CreateResponse(
                    HttpStatusCode.BadRequest, creditCard, "credit card already exists");
            }

            try
            {                           
                this.StoreAccountManager.Add(creditCard);
                this.StoreAccountManager.Save();

                return this.Request.CreateResponse(HttpStatusCode.Created, creditCard);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, creditCard);
            }
        }

        // PUT: api/CreditCardsApi/5
        [HttpPut]
        [Route("api/v1/CreditCardsApi")]
        public HttpResponseMessage Put([FromBody] CreditCard creditCard)
        {
            if (creditCard.CreditCardTypeId == 0 || string.IsNullOrEmpty(creditCard.CardNumber))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, creditCard);
            }

            try
            {
                var creditCardToUpdate = this.StoreAccountManager.GetById<CreditCard>(creditCard.CreditCardId);

                creditCardToUpdate.CreditCardTypeId = creditCard.CreditCardTypeId;
                creditCardToUpdate.CardNumber = creditCard.CardNumber;

                this.StoreAccountManager.Save();

                return this.Request.CreateResponse(HttpStatusCode.Accepted, creditCardToUpdate);
            }
            catch (Exception)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, creditCard);
            }
        }

        // DELETE: api/CreditCardsApi/5
        [HttpDelete]
        [Route("api/v1/CreditCardsApi/{id}")]
        public void Delete([FromUri]int? id)
        {
            var creditCard = this.StoreAccountManager.GetById<CreditCard>(id);
            if (creditCard != null)
            {
                this.StoreAccountManager.Delete(creditCard);
                this.StoreAccountManager.Save();
            }
        }        
    }
}
