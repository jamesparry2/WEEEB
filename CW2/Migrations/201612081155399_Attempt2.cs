namespace CW2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attempt2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anouncements", "isRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Anouncements", "isRead");
        }
    }
}
