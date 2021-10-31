using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ChatHistoryDTO
    {
        public int ChatHistId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ConversationId { get; set; }

        public virtual UserDTO FromUser { get; set; }

        public virtual UserDTO ToUser { get; set; }

    }
}
