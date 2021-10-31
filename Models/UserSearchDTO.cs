using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserSearchDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public string ProfilePicUrl { get; set; }
        //public string SearchText { get; set; } = "";
        public List<UserActivityDTO> UserActivityList { get; set; }
    }
}
