namespace CW2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "CommentDes", c => c.String(nullable: false, maxLength: 120));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "CommentDes", c => c.String());
        }
    }
}
