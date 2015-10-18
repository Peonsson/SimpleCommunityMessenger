using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SimpleCommunityMessager.Models
{
    public class GroupUser
    {
        public int Id { get; set; }
        [Required]
        public virtual Group Group { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
    }

    public class GroupUserDBContext : DbContext
    {
        public DbSet<GroupUser> GroupUsers { get; set; }
    }
}