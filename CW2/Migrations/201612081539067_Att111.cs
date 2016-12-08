namespace CW2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Att111 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StudentReads", "Annoce");
            DropColumn("dbo.StudentReads", "User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentReads", "User", c => c.String());
            AddColumn("dbo.StudentReads", "Annoce", c => c.Int(nullable: false));
        }
    }
}
