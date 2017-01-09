namespace Go2MusicStore.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMigrationProjectNameGo2MusicStorePlatform : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "ReviewDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Review", "ReviewDate");
        }
    }
}
