using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleCommunityMessager.Models
{
    // Create a new post
    public class CreateNewPostViewModel
    {
        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Receiver")]
        public string Receiver { get; set; }
    }

    // Show all users who has sent a post to a specific user
    public class ReceivedPostsOverviewViewModel
    {
        [Required]
        public List<string> Usernames { get; set; }

        [Required]
        [Display(Name = "Total no. of Messages")]
        public int TotalMessages { get; set; }

        [Required]
        [Display(Name = "Total no. of read Messages")]
        public int ReadMessages { get; set; }

        [Required]
        [Display(Name = "Total no. of deleted Messages")]
        public int DeletedMessages { get; set; }
    }

    // Show summary of a posts from a specific user
    public class ReceivedPostSummaryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Received at")]
        public DateTime Timestamp { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
    }

    // Show a specific message in detail
    public class ReceivedPostDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "Received at")]
        public DateTime Timestamp { get; set; }
    }

    // Create a new multicast post
    public class CreateNewMulticastPostViewModel
    {
        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Receivers")]
        public string Receivers { get; set; }
    }
}