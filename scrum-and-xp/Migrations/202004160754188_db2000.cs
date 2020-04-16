namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2000 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "CategoryId_Id", "dbo.Categories");
            DropIndex("dbo.Posts", new[] { "CategoryId_Id" });
            AddColumn("dbo.Posts", "CategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "CategoryId_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "CategoryId_Id", c => c.Int());
            DropColumn("dbo.Posts", "CategoryId");
            CreateIndex("dbo.Posts", "CategoryId_Id");
            AddForeignKey("dbo.Posts", "CategoryId_Id", "dbo.Categories", "Id");
        }
    }
}
