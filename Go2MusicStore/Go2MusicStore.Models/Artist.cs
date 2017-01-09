namespace Go2MusicStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Artist
    {
        public int ArtistId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        [Required]
        public int GenreId { get; set; }
        
        public virtual Genre Genre { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}