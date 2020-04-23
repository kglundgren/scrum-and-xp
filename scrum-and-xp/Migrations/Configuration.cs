namespace scrum_and_xp.Migrations
{
    using scrum_and_xp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<scrum_and_xp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(scrum_and_xp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.FormalTypes.AddOrUpdate(x => x.Id, 
                new FormalType() { Name = "Work" }, 
                new FormalType() { Name = "Research" }, 
                new FormalType() { Name = "Education" });
            context.SaveChanges();

            context.InformalCategories.AddOrUpdate(x => x.Id,
                new InformalCategory() { Name = "Animals" });
            context.SaveChanges();

            context.FormalCategories.AddOrUpdate(x => x.Id,
                new FormalCategory() { Name = "Conference", Type = context.FormalTypes.Single(i => i.Name =="Work") });
            context.SaveChanges();
        }
    }
}
