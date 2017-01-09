namespace MyAlbumStore.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumsAddedToGenreModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Album", "GenreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Album", "GenreId");
            AddForeignKey("dbo.Album", "GenreId", "dbo.Genre", "GenreId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Album", "GenreId", "dbo.Genre");
            DropIndex("dbo.Album", new[] { "GenreId" });
            DropColumn("dbo.Album", "GenreId");
        }
    }
}
