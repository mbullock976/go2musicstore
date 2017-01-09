namespace Go2MusicStore.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_EcommerceModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.CreditCard",
                c => new
                    {
                        CreditCardId = c.Int(nullable: false, identity: true),
                        CreditCardTypeId = c.Int(nullable: false),
                        CardNumber = c.String(),
                        StoreAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CreditCardId)
                .ForeignKey("dbo.CreditCardType", t => t.CreditCardTypeId, cascadeDelete: true)
                .ForeignKey("dbo.StoreAccount", t => t.StoreAccountId, cascadeDelete: true)
                .Index(t => t.CreditCardTypeId)
                .Index(t => t.StoreAccountId);
            
            CreateTable(
                "dbo.CreditCardType",
                c => new
                    {
                        CreditCardTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CreditCardTypeId);
            
            CreateTable(
                "dbo.StoreAccount",
                c => new
                    {
                        StoreAccountId = c.Int(nullable: false, identity: true),
                        UserIdentityName = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        TelephoneNo = c.String(),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PostCode = c.String(nullable: false),
                        CountryId = c.Int(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        ShoppingCartId = c.Int(),
                    })
                .PrimaryKey(t => t.StoreAccountId)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.ShoppingCart",
                c => new
                    {
                        ShoppingCartId = c.Int(nullable: false, identity: true),
                        StoreAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShoppingCartId)
                .ForeignKey("dbo.StoreAccount", t => t.ShoppingCartId)
                .Index(t => t.ShoppingCartId);
            
            CreateTable(
                "dbo.ShoppingCartItem",
                c => new
                    {
                        ShoppingCartItemId = c.Int(nullable: false, identity: true),
                        ShoppingCartId = c.Int(nullable: false),
                        AlbumId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShoppingCartItemId)
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingCart", t => t.ShoppingCartId, cascadeDelete: true)
                .Index(t => t.ShoppingCartId)
                .Index(t => t.AlbumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCart", "ShoppingCartId", "dbo.StoreAccount");
            DropForeignKey("dbo.ShoppingCartItem", "ShoppingCartId", "dbo.ShoppingCart");
            DropForeignKey("dbo.ShoppingCartItem", "AlbumId", "dbo.Album");
            DropForeignKey("dbo.CreditCard", "StoreAccountId", "dbo.StoreAccount");
            DropForeignKey("dbo.StoreAccount", "CountryId", "dbo.Country");
            DropForeignKey("dbo.CreditCard", "CreditCardTypeId", "dbo.CreditCardType");
            DropIndex("dbo.ShoppingCartItem", new[] { "AlbumId" });
            DropIndex("dbo.ShoppingCartItem", new[] { "ShoppingCartId" });
            DropIndex("dbo.ShoppingCart", new[] { "ShoppingCartId" });
            DropIndex("dbo.StoreAccount", new[] { "CountryId" });
            DropIndex("dbo.CreditCard", new[] { "StoreAccountId" });
            DropIndex("dbo.CreditCard", new[] { "CreditCardTypeId" });
            DropTable("dbo.ShoppingCartItem");
            DropTable("dbo.ShoppingCart");
            DropTable("dbo.StoreAccount");
            DropTable("dbo.CreditCardType");
            DropTable("dbo.CreditCard");
            DropTable("dbo.Country");
        }
    }
}
