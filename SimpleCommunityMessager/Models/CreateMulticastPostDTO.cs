﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCommunityMessager.Models
{
    public class CreateMulticastPostDTO
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public int ReceiverGroup { get; set; }
    }
}