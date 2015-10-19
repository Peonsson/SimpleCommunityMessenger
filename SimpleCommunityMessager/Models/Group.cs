using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SimpleCommunityMessager.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class GroupDBContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
    }
}