using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SimpleCommunityMessager.Models
{
    public class Post
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
        public bool Read { get; set; }

        [Required]
        public bool Deleted { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public virtual ApplicationUser Receiver { get; set; }
    }

    public class PostDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
}