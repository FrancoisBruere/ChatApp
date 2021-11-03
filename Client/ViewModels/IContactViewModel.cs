using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public interface IContactViewModel
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }


        public Task<string> AddContact(UserContactsDTO contacts);

        public Task<IEnumerable<UserContactsDTO>> GetContacts(string email);
        
    }
}
