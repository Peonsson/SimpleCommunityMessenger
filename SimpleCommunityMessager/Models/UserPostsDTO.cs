using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{
    public class UserPostsDTO
    {
        public List<string> usernames { get; set; }

        public int totalMessages { get; set; }

        public int unreadMessages { get; set; }

        public int deletedMessages { get; set; }

    }
}