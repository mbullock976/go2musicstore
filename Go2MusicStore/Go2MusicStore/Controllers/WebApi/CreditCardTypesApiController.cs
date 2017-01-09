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

    public class CreditCardTypesApiController : BaseApiController
    {
        public CreditCardTypesApiController(IApplicationManager applicationManager)
           : base(applicationManager)
        {
        }

        // GET: api/CreditCardTypes
        public IEnumerable<CreditCardType> Get()
        {
            return this.StoreAccountManager.Get<CreditCardType>();
        }

        // GET: api/CreditCardTypes/5
        public CreditCardType Get(int creditCardTypeId)
        {
            return this.StoreAccountManager.GetById<CreditCardType>(creditCardTypeId);
        }
    }
}
