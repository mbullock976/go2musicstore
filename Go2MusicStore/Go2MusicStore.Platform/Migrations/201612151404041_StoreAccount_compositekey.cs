namespace Go2MusicStore.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreAccount_compositekey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreditCard", "StoreAccountId", "dbo.StoreAccount");
            DropForeignKey("dbo.ShoppingCart", "ShoppingCartId", "dbo.StoreAccount");
            DropIndex("dbo.CreditCard", new[] { "StoreAccountId" });
            DropIndex("dbo.ShoppingCart", new[] { "ShoppingCartId" });
            DropPrimaryKey("dbo.StoreAccount");
            AddColumn("dbo.CreditCard", "StoreAccount_StoreAccountId", c => c.Int());
            AddColumn("dbo.CreditCard", "StoreAccount_UserIdentityName", c => c.String(maxLength: 128));
            AddColumn("dbo.ShoppingCart", "StoreAccount_StoreAccountId", c => c.Int(nullable: false));
            AddColumn("dbo.ShoppingCart", "StoreAccount_UserIdentityName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StoreAccount", "StoreAccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.StoreAccount", "UserIdentityName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.StoreAccount", new[] { "StoreAccountId", "UserIdentityName" });
            CreateIndex("dbo.CreditCard", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" });
            CreateIndex("dbo.ShoppingCart", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" });
            AddForeignKey("dbo.CreditCard", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount", new[] { "StoreAccountId", "UserIdentityName" });
            AddForeignKey("dbo.ShoppingCart", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount", new[] { "StoreAccountId", "UserIdentityName" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCart", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount");
            DropForeignKey("dbo.CreditCard", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount");
            DropIndex("dbo.ShoppingCart", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" });
            DropIndex("dbo.CreditCard", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" });
            DropPrimaryKey("dbo.StoreAccount");
            AlterColumn("dbo.StoreAccount", "UserIdentityName", c => c.String());
            AlterColumn("dbo.StoreAccount", "StoreAccountId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.ShoppingCart", "StoreAccount_UserIdentityName");
            DropColumn("dbo.ShoppingCart", "StoreAccount_StoreAccountId");
            DropColumn("dbo.CreditCard", "StoreAccount_UserIdentityName");
            DropColumn("dbo.CreditCard", "StoreAccount_StoreAccountId");
            AddPrimaryKey("dbo.StoreAccount", "StoreAccountId");
            CreateIndex("dbo.ShoppingCart", "ShoppingCartId");
            CreateIndex("dbo.CreditCard", "StoreAccountId");
            AddForeignKey("dbo.ShoppingCart", "ShoppingCartId", "dbo.StoreAccount", "StoreAccountId");
            AddForeignKey("dbo.CreditCard", "StoreAccountId", "dbo.StoreAccount", "StoreAccountId", cascadeDelete: true);
        }
    }
}
