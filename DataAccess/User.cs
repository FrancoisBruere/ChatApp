using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class User
    {
        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Source { get; set; }
        public string ExternalUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicUrl { get; set; }
        public string About { get; set; }
        public int Notifications { get; set; }
        public int DarkTheme { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        
    }
}
