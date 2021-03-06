﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace scrum_and_xp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public byte Img { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<FormalPost> FormalPosts { get; set; }
        public DbSet<InformalPost> InformalPosts { get; set; }
        public DbSet<FormalType> FormalTypes { get; set; }
        public DbSet<FormalCategory> FormalCategories { get; set; }
        public DbSet<InformalCategory> InformalCategories { get; set; }
        public DbSet<SchedulerEvent> SchedulerEvents { get; set; }
        public DbSet<UpcomingMeeting> UpcomingMeetings { get; set; }
        public DbSet<UsersUpcomingMeetings> UsersUpcomingMeetings { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

      
    }

    
}