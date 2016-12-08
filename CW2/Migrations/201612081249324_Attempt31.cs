namespace CW2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attempt31 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentReadViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnnounceId_Id = c.Int(),
                        UserId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Anouncements", t => t.AnnounceId_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId_Id)
                .Index(t => t.AnnounceId_Id)
                .Index(t => t.UserId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentReadViews", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentReadViews", "AnnounceId_Id", "dbo.Anouncements");
            DropIndex("dbo.StudentReadViews", new[] { "UserId_Id" });
            DropIndex("dbo.StudentReadViews", new[] { "AnnounceId_Id" });
            DropTable("dbo.StudentReadViews");
        }
    }
}
