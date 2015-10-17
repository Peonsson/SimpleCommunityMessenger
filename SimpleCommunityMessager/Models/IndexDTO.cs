using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCommunityMessager.Models
{
    public class IndexDTO
    {

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