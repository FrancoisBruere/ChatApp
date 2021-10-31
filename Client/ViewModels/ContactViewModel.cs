using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public class ContactViewModel : IContactViewModel
    {

        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }


        private HttpClient _httpClient;

        public ContactViewModel()
        {

        }

        public ContactViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AddContact(UserContactsDTO contacts)
        {
            try
            {


                if (contacts != null)
                {
                    var responce = await _httpClient.PostAsJsonAsync("Contact/addContact", contacts);
                    var result = await responce.Content.ReadAsStringAsync();


                    return result;
                }

                return null;

            }
            catch (Exception u)
            {

                throw new Exception(u.Message);
            }
        }

        public async Task<IEnumerable<UserContactsDTO>> GetContacts(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync("Contact/getContacts/" + email);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myContacts = JsonConvert.DeserializeObject<IEnumerable<UserContactsDTO>>(content);

                    return myContacts;
                }

                return null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static implicit operator ContactViewModel(UserContactsDTO contact)
        {
            return new ContactViewModel
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                UserId = contact.UserId,
            };


        }

        public static implicit operator UserContactsDTO(ContactViewModel contactViewModel)
        {

            return new UserContactsDTO
            {
                FirstName = contactViewModel.FirstName,
                LastName = contactViewModel.LastName,
                Email = contactViewModel.Email,
                UserId = contactViewModel.UserId,


            };

        }
    }
}
