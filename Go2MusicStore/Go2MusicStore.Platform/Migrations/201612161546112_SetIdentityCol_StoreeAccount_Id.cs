namespace Go2MusicStore.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetIdentityCol_StoreeAccount_Id : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreditCard", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount");
            DropForeignKey("dbo.ShoppingCart", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount");
            DropPrimaryKey("dbo.StoreAccount");
            AlterColumn("dbo.StoreAccount", "StoreAccountId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.StoreAccount", new[] { "StoreAccountId", "UserIdentityName" });
            AddForeignKey("dbo.CreditCard", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount", new[] { "StoreAccountId", "UserIdentityName" });
            AddForeignKey("dbo.ShoppingCart", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount", new[] { "StoreAccountId", "UserIdentityName" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCart", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount");
            DropForeignKey("dbo.CreditCard", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount");
            DropPrimaryKey("dbo.StoreAccount");
            AlterColumn("dbo.StoreAccount", "StoreAccountId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.StoreAccount", new[] { "StoreAccountId", "UserIdentityName" });
            AddForeignKey("dbo.ShoppingCart", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount", new[] { "StoreAccountId", "UserIdentityName" });
            AddForeignKey("dbo.CreditCard", new[] { "StoreAccount_StoreAccountId", "StoreAccount_UserIdentityName" }, "dbo.StoreAccount", new[] { "StoreAccountId", "UserIdentityName" });
        }
    }
}
