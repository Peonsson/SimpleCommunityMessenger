using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{
    // A message sent to a group
    public class GroupMessageViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }

    // Used when creating new group messages
    public class CreateGroupMessageViewModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public int ReceiverGroup { get; set; }
    }
}