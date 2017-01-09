namespace Go2MusicStore.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAlbumAndReviewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        StockCount = c.Int(nullable: false),
                        TotalStarRating = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Artist", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 150),
                        StarRating = c.Int(nullable: false),
                        IsRecommended = c.Boolean(nullable: false),
                        AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "AlbumId", "dbo.Album");
            DropForeignKey("dbo.Album", "ArtistId", "dbo.Artist");
            DropIndex("dbo.Review", new[] { "AlbumId" });
            DropIndex("dbo.Album", new[] { "ArtistId" });
            DropTable("dbo.Review");
            DropTable("dbo.Album");
        }
    }
}
