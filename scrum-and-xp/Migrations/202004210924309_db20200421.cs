namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db20200421 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormalPosts", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.InformalPosts", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InformalPosts", "CategoryId");
            DropColumn("dbo.FormalPosts", "CategoryId");
        }
    }
}
