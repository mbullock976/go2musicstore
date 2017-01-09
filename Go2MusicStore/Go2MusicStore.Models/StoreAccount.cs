namespace Go2MusicStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.Contracts;

    public class StoreAccount
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StoreAccountId { get; set; }

        public string UserIdentityName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string TelephoneNo { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public virtual ICollection<CreditCard> CreditCards { get; set; }
        
        public int? ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

    }
}