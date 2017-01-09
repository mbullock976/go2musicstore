namespace Go2MusicStore.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseOrderModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseOrderItem",
                c => new
                    {
                        PurchaseOrderItemId = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
                        AlbumId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseOrderItemId)
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false, identity: true),
                        StoreAccountId = c.Int(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        TotalOrderAmount = c.Double(nullable: false),
                        StoreAccount_StoreAccountId = c.Int(),
                        StoreAccount_UserIdentityName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PurchaseOrderId)
                .ForeignKey("dbo.StoreAccount", t => new { t.StoreAccount_StoreAccountId, t.StoreAccount_UserIdentityName })
                .Index(t => new { t.StoreAccount_StoreAccountId, t.StoreAccount_UserIdentityName });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrder", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount");
            DropForeignKey("dbo.PurchaseOrderItem", "PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrderItem", "AlbumId", "dbo.Album");
            DropIndex("dbo.PurchaseOrder", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" });
            DropIndex("dbo.PurchaseOrderItem", new[] { "AlbumId" });
            DropIndex("dbo.PurchaseOrderItem", new[] { "PurchaseOrderId" });
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.PurchaseOrderItem");
        }
    }
}
