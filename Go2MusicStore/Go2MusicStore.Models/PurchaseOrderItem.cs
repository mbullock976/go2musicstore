namespace Go2MusicStore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PurchaseOrderItem
    {
        public int PurchaseOrderItemId { get; set; }

        public int PurchaseOrderId { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public double TotalAmount { get; set; }
    }
}