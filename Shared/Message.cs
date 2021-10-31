using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared
{
   
        public class Message
        {
            public string ToUserId { get; set; }
            public string FromUserId { get; set; }
            public string MessageText { get; set; }
            public DateTime CreatedDate { get; set; }

            public string ConversationId { get; set; }
        }

    
}
