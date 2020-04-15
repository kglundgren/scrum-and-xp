namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "AuthorId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "AuthorId_Id" });
            CreateTable(
                "dbo.FormalPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        postTime = c.DateTime(nullable: false),
                        AuthorFirstName = c.String(),
                        AuthorLastName = c.String(),
                        AuthorId_Id = c.String(maxLength: 128),
                        FormalCat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId_Id)
                .ForeignKey("dbo.FormalCategories", t => t.FormalCat_Id)
                .Index(t => t.AuthorId_Id)
                .Index(t => t.FormalCat_Id);
            
            CreateTable(
                "dbo.InformalPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        postTime = c.DateTime(nullable: false),
                        AuthorFirstName = c.String(),
                        AuthorLastName = c.String(),
                        AuthorId_Id = c.String(maxLength: 128),
                        InformalCat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId_Id)
                .ForeignKey("dbo.InformalCategories", t => t.InformalCat_Id)
                .Index(t => t.AuthorId_Id)
                .Index(t => t.InformalCat_Id);
            
            DropTable("dbo.Posts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        postTime = c.DateTime(nullable: false),
                        AuthorId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.InformalPosts", "InformalCat_Id", "dbo.InformalCategories");
            DropForeignKey("dbo.InformalPosts", "AuthorId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FormalPosts", "FormalCat_Id", "dbo.FormalCategories");
            DropForeignKey("dbo.FormalPosts", "AuthorId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.InformalPosts", new[] { "InformalCat_Id" });
            DropIndex("dbo.InformalPosts", new[] { "AuthorId_Id" });
            DropIndex("dbo.FormalPosts", new[] { "FormalCat_Id" });
            DropIndex("dbo.FormalPosts", new[] { "AuthorId_Id" });
            DropTable("dbo.InformalPosts");
            DropTable("dbo.FormalPosts");
            CreateIndex("dbo.Posts", "AuthorId_Id");
            AddForeignKey("dbo.Posts", "AuthorId_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
