using SimpleCommunityMessager.Models;
using System;
using System.Data.Entity;

namespace SimpleCommunityMessager.Models
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Deleted { get; set; }
        public virtual Group Group { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }

    public class GroupMessageDBContext : DbContext
    {
        public DbSet<GroupMessage> GroupMessages { get; set; }
    }
}

