namespace Go2MusicStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }

        public int StoreAccountId { get; set; }

        public virtual StoreAccount StoreAccount { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}