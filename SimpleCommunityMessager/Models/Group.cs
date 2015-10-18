using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SimpleCommunityMessager.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }

    public class GroupDBContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
    }
}