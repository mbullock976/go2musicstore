namespace Go2MusicStore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CreditCard
    {
        public int CreditCardId { get; set; }

        public int CreditCardTypeId { get; set; }

        public virtual CreditCardType CardType { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }

        public int StoreAccountId { get; set; }
        
        public virtual StoreAccount StoreAccount { get; set; }
    }
}