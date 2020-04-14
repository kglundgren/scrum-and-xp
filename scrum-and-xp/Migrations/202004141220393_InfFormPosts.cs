namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InfFormPosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "AuthorFirstName", c => c.String());
            AddColumn("dbo.Posts", "AuthorLastName", c => c.String());
            AddColumn("dbo.Posts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Posts", "Category_Id", c => c.Int());
            AddColumn("dbo.Posts", "Category_Id1", c => c.Int());
            CreateIndex("dbo.Posts", "Category_Id");
            CreateIndex("dbo.Posts", "Category_Id1");
            AddForeignKey("dbo.Posts", "Category_Id", "dbo.FormalCategories", "Id");
            AddForeignKey("dbo.Posts", "Category_Id1", "dbo.InformalCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Category_Id1", "dbo.InformalCategories");
            DropForeignKey("dbo.Posts", "Category_Id", "dbo.FormalCategories");
            DropIndex("dbo.Posts", new[] { "Category_Id1" });
            DropIndex("dbo.Posts", new[] { "Category_Id" });
            DropColumn("dbo.Posts", "Category_Id1");
            DropColumn("dbo.Posts", "Category_Id");
            DropColumn("dbo.Posts", "Discriminator");
            DropColumn("dbo.Posts", "AuthorLastName");
            DropColumn("dbo.Posts", "AuthorFirstName");
        }
    }
}
