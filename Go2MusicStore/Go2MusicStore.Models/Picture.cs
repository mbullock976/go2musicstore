namespace Go2MusicStore.Models
{
    using System.Web;

    public class Picture
    {
        public int PictureId { get; set; }

        public int AlbumId { get; set; }

        public int ArtistId { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}