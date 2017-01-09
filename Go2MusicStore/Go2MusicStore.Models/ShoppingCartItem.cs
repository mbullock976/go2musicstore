namespace Go2MusicStore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Migrations.Design;

    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public int AlbumId { get; set; }
        
        public virtual Album Album { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }
    }
}