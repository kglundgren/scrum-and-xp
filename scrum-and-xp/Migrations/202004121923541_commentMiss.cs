namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentMiss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormalCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FormalCustoms", "FormalCategory_Id", c => c.Int());
            CreateIndex("dbo.FormalCustoms", "FormalCategory_Id");
            AddForeignKey("dbo.FormalCustoms", "FormalCategory_Id", "dbo.FormalCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormalCustoms", "FormalCategory_Id", "dbo.FormalCategories");
            DropIndex("dbo.FormalCustoms", new[] { "FormalCategory_Id" });
            DropColumn("dbo.FormalCustoms", "FormalCategory_Id");
            DropTable("dbo.FormalCategories");
        }
    }
}
