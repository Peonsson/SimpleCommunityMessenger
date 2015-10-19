using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{
    // Create a new post
    public class CreateNewPostViewModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Receiver { get; set; }
    }

    // Show all users who has sent a post to a specific user
    public class ReceivedPostsOverviewViewModel
    {
        public List<string> Usernames { get; set; }
        public int TotalMessages { get; set; }
        public int ReadMessages { get; set; }
        public int DeletedMessages { get; set; }
    }

    // Show summary of a posts from a specific user
    public class ReceivedPostSummaryViewModel
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Subject { get; set; }
    }

    // Show a specific message in detail
    public class ReceivedPostDetailsViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}