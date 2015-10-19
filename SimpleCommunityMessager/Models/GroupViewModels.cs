using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleCommunityMessager.Models
{
    // For showing a list all groups
    public class GroupListViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public bool Member { get; set; }
    }

    // Shows the summary of messages sent to a group
    public class ReceivedGroupPostSummaryViewModel
    {
        // Group id
        public int Id { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public List<ReceivedGroupPostBriefViewModel> messages { get; set; }
    }

    // Used for showing a list of all messages in a group
    public class ReceivedGroupPostBriefViewModel
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

    // Used to create a group
    public class CreateGroupViewModel
    {
        [Required]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "The {0} must be atleast {2} characters long.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}