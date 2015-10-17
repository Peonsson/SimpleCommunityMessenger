using System;
using System.Data.Entity;

namespace SimpleCommunityMessager.Models
{
    public class Post
    {
        public int ID { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Read { get; set; }
        public bool Deleted { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
    }

    public class PostDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
}