namespace Go2MusicStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Security.Permissions;

    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }

        public int StoreAccountId { get; set; }        

        public virtual StoreAccount StoreAccount { get; set; }

        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }

        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }

        [DataType(DataType.Currency)]
        public double TotalOrderAmount { get; set; }
    }
}