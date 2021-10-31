using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared
{
    public class UserChatDetails
    {

        public string ChatFromId { get; set; }
        public string ChatToId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string ChatStatus { get; set; }

        public string UserHubConnectId { get; set; }
    }
}
