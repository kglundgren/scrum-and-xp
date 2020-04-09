namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postProps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Title", c => c.String());
            AddColumn("dbo.Posts", "AuthorId_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "AuthorId_Id");
            AddForeignKey("dbo.Posts", "AuthorId_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "AuthorId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "AuthorId_Id" });
            DropColumn("dbo.Posts", "AuthorId_Id");
            DropColumn("dbo.Posts", "Title");
        }
    }
}
