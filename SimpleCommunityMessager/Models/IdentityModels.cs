using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace SimpleCommunityMessager.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }

        public DateTime LastLogin { get; set; }
        public int LoginCounter { get; set; }

        // Navigation properties
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<GroupUser> GroupUser { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<GroupMessage> GroupMessages { get; set; }


        public ApplicationUser()
        {
            Groups = new List<Group>();
            GroupUser = new List<GroupUser>();
            Posts = new List<Post>();
            GroupMessages = new List<GroupMessage>();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
    }
}