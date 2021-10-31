using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserActivityDTO
    {
        public int ActivityId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public string OnlineOfflineStatus { get; set; }
        public int UserId { get; set; }

        public virtual UserDTO UserActivity { get; set; }

    }
}
