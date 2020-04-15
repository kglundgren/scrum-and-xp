namespace scrum_and_xp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bilder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Img", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Img");
        }
    }
}
