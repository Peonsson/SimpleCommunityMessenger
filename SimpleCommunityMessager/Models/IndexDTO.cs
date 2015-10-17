using System;

namespace SimpleCommunityMessager.Models
{
    public class IndexDTO
    {
        public int unreadCounter { get; set; }
        public string userName { get; set; }
        public DateTime lastLogin { get; set; }
        public int loginCounter { get; set; }
    }
}