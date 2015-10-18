using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{
    public class UserPostsDTO
    {
        public string username { get; set; }

        [Display(Name = "No. unread messages")]
        public int unreadCounter { get; set; }

        [Display(Name = "Username")]
        public string userName { get; set; }

        [Display(Name = "Last login date")]
        public DateTime lastLogin { get; set; }

        [Display(Name = "No. times user have logged in")]
        public int loginCounter { get; set; }
    }
}