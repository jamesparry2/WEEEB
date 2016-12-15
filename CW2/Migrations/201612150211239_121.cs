namespace CW2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _121 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "announc_Id" });
            CreateIndex("dbo.Comments", "Announc_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "Announc_Id" });
            CreateIndex("dbo.Comments", "announc_Id");
        }
    }
}
