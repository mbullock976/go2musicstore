namespace Go2MusicStore.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creditcard_cardNumber_required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreditCard", "CardNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CreditCard", "CardNumber", c => c.String());
        }
    }
}
