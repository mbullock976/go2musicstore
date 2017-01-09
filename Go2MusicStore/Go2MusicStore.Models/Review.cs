namespace Go2MusicStore.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        public string Comment { get; set; }

        [Required]
        [Range(0, 5)]
        public int StarRating { get; set; }

        public bool IsRecommended { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ReviewDate { get; set; }

        [Required]
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }

    }
}