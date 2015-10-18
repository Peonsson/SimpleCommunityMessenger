using SimpleCommunityMessager.Models;
using System;
using System.Data.Entity;

namespace SimpleCommunityMessager.Models
{
    public class MulticastPost
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Deleted { get; set; }
        public virtual Group Group { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }

    public class MulticastPostDBContext : DbContext
    {
        public DbSet<MulticastPost> MulticastPosts { get; set; }
    }
}

