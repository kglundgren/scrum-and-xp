namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class post_cat_foreignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "CategoryId_Id", c => c.Int());
            CreateIndex("dbo.Posts", "CategoryId_Id");
            AddForeignKey("dbo.Posts", "CategoryId_Id", "dbo.Categories", "Id");
            DropColumn("dbo.Posts", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Type", c => c.String());
            DropForeignKey("dbo.Posts", "CategoryId_Id", "dbo.Categories");
            DropIndex("dbo.Posts", new[] { "CategoryId_Id" });
            DropColumn("dbo.Posts", "CategoryId_Id");
        }
    }
}
