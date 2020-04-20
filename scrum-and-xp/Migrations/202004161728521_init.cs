namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormalCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormalTypes", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.FormalPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        PostTime = c.DateTime(nullable: false),
                        AuthorId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId_Id)
                .Index(t => t.AuthorId_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.FormalTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InformalCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InformalPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        PostTime = c.DateTime(nullable: false),
                        AuthorId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId_Id)
                .Index(t => t.AuthorId_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.FormalPostFormalCategories",
                c => new
                    {
                        FormalPost_Id = c.Int(nullable: false),
                        FormalCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FormalPost_Id, t.FormalCategory_Id })
                .ForeignKey("dbo.FormalPosts", t => t.FormalPost_Id, cascadeDelete: true)
                .ForeignKey("dbo.FormalCategories", t => t.FormalCategory_Id, cascadeDelete: true)
                .Index(t => t.FormalPost_Id)
                .Index(t => t.FormalCategory_Id);
            
            CreateTable(
                "dbo.InformalPostInformalCategories",
                c => new
                    {
                        InformalPost_Id = c.Int(nullable: false),
                        InformalCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InformalPost_Id, t.InformalCategory_Id })
                .ForeignKey("dbo.InformalPosts", t => t.InformalPost_Id, cascadeDelete: true)
                .ForeignKey("dbo.InformalCategories", t => t.InformalCategory_Id, cascadeDelete: true)
                .Index(t => t.InformalPost_Id)
                .Index(t => t.InformalCategory_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.InformalPostInformalCategories", "InformalCategory_Id", "dbo.InformalCategories");
            DropForeignKey("dbo.InformalPostInformalCategories", "InformalPost_Id", "dbo.InformalPosts");
            DropForeignKey("dbo.InformalPosts", "AuthorId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FormalCategories", "Type_Id", "dbo.FormalTypes");
            DropForeignKey("dbo.FormalPostFormalCategories", "FormalCategory_Id", "dbo.FormalCategories");
            DropForeignKey("dbo.FormalPostFormalCategories", "FormalPost_Id", "dbo.FormalPosts");
            DropForeignKey("dbo.FormalPosts", "AuthorId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.InformalPostInformalCategories", new[] { "InformalCategory_Id" });
            DropIndex("dbo.InformalPostInformalCategories", new[] { "InformalPost_Id" });
            DropIndex("dbo.FormalPostFormalCategories", new[] { "FormalCategory_Id" });
            DropIndex("dbo.FormalPostFormalCategories", new[] { "FormalPost_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.InformalPosts", new[] { "AuthorId_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.FormalPosts", new[] { "AuthorId_Id" });
            DropIndex("dbo.FormalCategories", new[] { "Type_Id" });
            DropTable("dbo.InformalPostInformalCategories");
            DropTable("dbo.FormalPostFormalCategories");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.InformalPosts");
            DropTable("dbo.InformalCategories");
            DropTable("dbo.FormalTypes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.FormalPosts");
            DropTable("dbo.FormalCategories");
        }
    }
}
