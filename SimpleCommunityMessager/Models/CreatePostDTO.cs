using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{
    public class CreatePostDTO
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Receiver { get; set; }
    }
}