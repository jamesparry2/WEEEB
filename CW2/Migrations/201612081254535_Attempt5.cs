namespace CW2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attempt5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentReadViews", "Annoce", c => c.Int(nullable: false));
            AddColumn("dbo.StudentReadViews", "UserReadId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentReadViews", "UserReadId");
            DropColumn("dbo.StudentReadViews", "Annoce");
        }
    }
}
