namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customTree : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FormalEducations", newName: "FormalCustoms");
            RenameTable(name: "dbo.FormalResearches", newName: "InformalCustoms");
            CreateTable(
                "dbo.InformalCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.InformalCustoms", "InformalCategory_Id", c => c.Int());
            CreateIndex("dbo.InformalCustoms", "InformalCategory_Id");
            AddForeignKey("dbo.InformalCustoms", "InformalCategory_Id", "dbo.InformalCategories", "Id");
            DropTable("dbo.FormalWorks");
            DropTable("dbo.InformalAWs");
            DropTable("dbo.InformalGenerals");
            DropTable("dbo.InformalTrips");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InformalTrips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InformalGenerals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InformalAWs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FormalWorks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.InformalCustoms", "InformalCategory_Id", "dbo.InformalCategories");
            DropIndex("dbo.InformalCustoms", new[] { "InformalCategory_Id" });
            DropColumn("dbo.InformalCustoms", "InformalCategory_Id");
            DropTable("dbo.InformalCategories");
            RenameTable(name: "dbo.InformalCustoms", newName: "FormalResearches");
            RenameTable(name: "dbo.FormalCustoms", newName: "FormalEducations");
        }
    }
}
