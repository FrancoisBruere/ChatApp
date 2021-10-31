using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public class UserSearchViewModel : IUserSearchViewModel
    {
        //int UserId { get; set; }
        //string Email { get; set; }
        //string FirstName { get; set; }
        //string LastName { get; set; }
        //string About { get; set; }

        private HttpClient _httpClient;

        public UserSearchViewModel()
        {

        }


        public UserSearchViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IEnumerable<UserSearchDTO>> GetAllUsersSearch()
        {
            try
            {
                var response = await _httpClient.GetAsync("Profile/getallprofiles");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var userActivities = JsonConvert.DeserializeObject<IEnumerable<UserSearchDTO>>(content);

                    return userActivities;
                }

                return null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
