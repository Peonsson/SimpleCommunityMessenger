using System.Collections.Generic;

namespace SimpleCommunityMessager.Models
{
    public class GroupDetailsDTO
    {
        public int Id { get; set; }
        public List<GroupDetailMessagesDTO> messages { get; set; }
    }
}