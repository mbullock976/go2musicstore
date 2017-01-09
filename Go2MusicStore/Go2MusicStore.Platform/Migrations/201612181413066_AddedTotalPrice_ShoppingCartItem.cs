namespace Go2MusicStore.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTotalPrice_ShoppingCartItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartItem", "TotalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCartItem", "TotalPrice");
        }
    }
}
