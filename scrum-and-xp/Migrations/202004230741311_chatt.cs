namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SchedulerEvents", "CreatorId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SchedulerEvents", new[] { "CreatorId_Id" });
            DropTable("dbo.SchedulerEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SchedulerEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreatorId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.SchedulerEvents", "CreatorId_Id");
            AddForeignKey("dbo.SchedulerEvents", "CreatorId_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
