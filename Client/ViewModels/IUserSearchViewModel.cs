using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public interface IUserSearchViewModel
    {
        //public int UserId { get; set; }
        //public string Email { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string About { get; set; }


        public Task<IEnumerable<UserSearchDTO>> GetAllUsersSearch();
    }
}
