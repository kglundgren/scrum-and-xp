namespace scrum_and_xp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var PasswordHasher = new PasswordHasher();

            var adminRole = new IdentityRole { Name = "Admin" };
            var usersRole = new IdentityRole { Name = "Users" };

            RoleManager.Create(adminRole);
            RoleManager.Create(usersRole);

            var user = new ApplicationUser
            {
                UserName = "admin@admin.admin",
                Email = "admin@admin.admin",
                PasswordHash = PasswordHasher.HashPassword("password"),
                FirstName = "Super",
                LastName = "Admin"
            };
            var userAdminRole = new IdentityUserRole();
            var userUserRole = new IdentityUserRole();
            userAdminRole.RoleId = adminRole.Id;
            userAdminRole.UserId = user.Id;
            userUserRole.RoleId = usersRole.Id;
            userUserRole.UserId = user.Id;

            user.Roles.Add(userAdminRole);
            user.Roles.Add(userUserRole);
            UserManager.Create(user);

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
