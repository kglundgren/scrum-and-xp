namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbCatChange : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FormalCategories", newName: "FormalEducations");
            RenameTable(name: "dbo.InformalCategories", newName: "FormalResearches");
            CreateTable(
                "dbo.FormalWorks",
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
                "dbo.InformalGenerals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InformalTrips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InformalTrips");
            DropTable("dbo.InformalGenerals");
            DropTable("dbo.InformalAWs");
            DropTable("dbo.FormalWorks");
            RenameTable(name: "dbo.FormalResearches", newName: "InformalCategories");
            RenameTable(name: "dbo.FormalEducations", newName: "FormalCategories");
        }
    }
}
