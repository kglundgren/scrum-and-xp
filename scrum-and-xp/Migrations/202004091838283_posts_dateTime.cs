namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posts_dateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "postTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "postTime");
        }
    }
}
