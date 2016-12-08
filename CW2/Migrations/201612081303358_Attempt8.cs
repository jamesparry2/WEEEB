namespace CW2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attempt8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentReads", "User", c => c.String());
            DropColumn("dbo.StudentReads", "UserReadId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentReads", "UserReadId", c => c.Int(nullable: false));
            DropColumn("dbo.StudentReads", "User");
        }
    }
}
