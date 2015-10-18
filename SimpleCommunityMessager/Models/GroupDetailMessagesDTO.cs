using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{
    public class GroupDetailMessagesDTO
    {
        public string Subject { get; set; }
        public DateTime Timestamp { get; set; }
        public int Id { get; set; } 
    }
}