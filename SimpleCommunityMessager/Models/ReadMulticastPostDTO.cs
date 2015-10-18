using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{
    public class ReadMulticastPostDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
    }
}