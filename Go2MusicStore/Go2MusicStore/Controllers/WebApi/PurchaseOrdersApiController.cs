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

    public class PurchaseOrdersApiController : BaseApiController
    {
        public PurchaseOrdersApiController(IApplicationManager applicationManager)
            : base(applicationManager)
        {
        }

        // GET: api/PurchaseOrdersApi
        [HttpGet]
        [Route("api/v1/PurchaseOrdersApi")]
        public IEnumerable<PurchaseOrder> Get()
        {
            return this.StoreAccountManager.Get<PurchaseOrder>();
        }

        // GET: api/PurchaseOrdersApi/5
        [HttpGet]
        [Route("api/v1/PurchaseOrdersApi/{id}")]
        public PurchaseOrder Get(int id)
        {
            return this.StoreAccountManager.GetById<PurchaseOrder>(id);
        }

        [HttpGet]
        [Route("api/v1/PurchaseOrdersApi/GetByStoreAccountId/{storeAccountid?}")]
        public IEnumerable<PurchaseOrder> GetByStoreAccountId(int? storeAccountId)
        {
            return this.StoreAccountManager.Get<PurchaseOrder>()
                .Where(m => m.StoreAccountId == storeAccountId)
                .OrderByDescending(m => m.PurchaseDate);
        }

        // POST: api/PurchaseOrdersApi
        [HttpPost]
        [Route("api/v1/PurchaseOrdersApi")]
        public HttpResponseMessage Post([FromBody] PurchaseOrder purchaseOrder)
        {
            try
            {
                purchaseOrder.PurchaseOrderId = 0;
                purchaseOrder.PurchaseDate = DateTime.UtcNow;
                
                this.StoreAccountManager.Add(purchaseOrder);
                this.StoreAccountManager.Save();

                return this.Request.CreateResponse(HttpStatusCode.Created, purchaseOrder);
            }
            catch (Exception)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, purchaseOrder);
            }
        }        
    }
}
