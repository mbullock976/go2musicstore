namespace Go2MusicStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        public int AlbumId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        public int StockCount { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        [Range(0, 5)]
        public int TotalStarRating { get; set; }

        [Required]
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }

        [DisplayName("Album Art URL")]
        [StringLength(1024)]
        public string AlbumArtUrl { get; set; }
    }
}