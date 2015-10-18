using System;

namespace SimpleCommunityMessager.Models
{
    public class ReadPostsFromUserDTO
    {
        public DateTime Timestamp { get; set; }
        public string Subject { get; set; }
        public int Id { get; set; }
    }
}