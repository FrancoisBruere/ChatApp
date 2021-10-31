using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserActivity
    {
        [Key]
        [Required]
        public int ActivityId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public string OnlineOfflineStatus { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserActivities { get; set; }

    }
}
