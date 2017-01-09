namespace Go2MusicStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }
}