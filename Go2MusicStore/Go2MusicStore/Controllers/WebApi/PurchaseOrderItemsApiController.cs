using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Go2MusicStore.Controllers.WebApi
{
    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.API.Interfaces.Services;
    using Go2MusicStore.Models;

    public class PurchaseOrderItemsApiController : BaseApiController
    {
        private readonly ISignalRService signalRService;

        public PurchaseOrderItemsApiController(
            IApplicationManager applicationManager,
            ISignalRService signalRService)
            : base(applicationManager)
        {
            this.signalRService = signalRService;
        }

        // GET: api/PurchaseOrderItemsApi
        [HttpGet]
        [Route("api/v1/PurchaseOrderItemsApi")]
        public IEnumerable<PurchaseOrderItem> Get()
        {
            return this.StoreAccountManager.Get<PurchaseOrderItem>();
        }

        // GET: api/PurchaseOrderItemsApi/5
        [HttpGet]
        [Route("api/v1/PurchaseOrderItemsApi/{id}")]
        public PurchaseOrderItem Get(int id)
        {
            return this.StoreAccountManager.GetById<PurchaseOrderItem>(id);
        }

        // POST: api/PurchaseOrderItemsApi
        [HttpPost]
        [Route("api/v1/PurchaseOrderItemsApi")]
        public HttpResponseMessage Post([FromBody]IEnumerable<PurchaseOrderItem> purchaseOrderItems)
        {
            try
            {
                foreach (var purchaseOrderItem in purchaseOrderItems)
                {
                    //deduct available stock upon purchase
                    var album = StoreAccountManager.GetById<Album>(purchaseOrderItem.AlbumId);
                    album.StockCount -= purchaseOrderItem.Quantity;
                    if (album.StockCount < 0)
                    {
                        album.StockCount = 0;
                    }

                    if (album.StockCount == 0)
                    {
                        this.signalRService.OutofStockSignal(album.AlbumId, true);
                    }
                        
                    this.StoreAccountManager.Save();

                    purchaseOrderItem.Album = null;
                    this.StoreAccountManager.Add(purchaseOrderItem);
                    this.StoreAccountManager.Save();                    
                }

                return this.Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }      
    }
}
