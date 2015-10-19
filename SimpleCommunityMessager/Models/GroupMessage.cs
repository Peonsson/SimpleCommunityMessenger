using SimpleCommunityMessager.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SimpleCommunityMessager.Models
{
    public class GroupMessage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "Received at")]
        public DateTime Timestamp { get; set; }

        [Required]
        public bool Deleted { get; set; }

        [Required]
        public virtual Group Group { get; set; }

        [Required]
        public virtual ApplicationUser Sender { get; set; }
    }

    public class GroupMessageDBContext : DbContext
    {
        public DbSet<GroupMessage> GroupMessages { get; set; }
    }
}

