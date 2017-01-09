namespace Go2MusicStore.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumAlbumArtistUrl_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Album", "AlbumArtUrl", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Album", "AlbumArtUrl");
        }
    }
}
