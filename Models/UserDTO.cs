using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class UserDTO
    {

        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string Source { get; set; }
        public string ExternalUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicUrl { get; set; }
        public string About { get; set; }
        public int Notifications { get; set; }
        public int DarkTheme { get; set; }
        
        

    }
}
