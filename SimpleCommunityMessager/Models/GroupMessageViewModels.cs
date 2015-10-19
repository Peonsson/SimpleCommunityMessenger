using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{
    // A message sent to a group
    public class GroupMessageViewModel
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

    // Used when creating new group messages
    public class CreateGroupMessageViewModel
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
        public int ReceiverGroup { get; set; }
    }
}