using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{
    public class GroupDTO
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool Member { get; set; }
    }
}