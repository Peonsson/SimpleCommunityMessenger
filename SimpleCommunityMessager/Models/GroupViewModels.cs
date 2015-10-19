using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{

    // For showing a list all groups
    public class GroupListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // Shows the summary of messages sent to a group
    public class ReceivedGroupPostSummaryViewModel
    {
        // Group id
        public int Id { get; set; }
        public List<ReceivedGroupPostBriefViewModel> messages { get; set; }
    }

    // Used for showing a list of all messages in a group
    public class ReceivedGroupPostBriefViewModel
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Subject { get; set; }
    }
}