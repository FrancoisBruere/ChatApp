using Common;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public class ActivityViewModel : IActivityViewModel
    {

      
        private HttpClient _httpClient;

        public ActivityViewModel()
        {

        }


        public ActivityViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddUserActivity(int userId)
        {
            if (userId != 0)
            {
                UserActivityDTO activityDTO = new UserActivityDTO();
                activityDTO.UserId = userId;
                activityDTO.OnlineOfflineStatus = SD.LocalStorage_ChatStatusOnline;
                activityDTO.LoginDate = DateTime.Now;
                
                var response = await _httpClient.PostAsJsonAsync("Activity/adduseractivity/",activityDTO);

                return true;
            }

            return false; 
            
        }

        public async Task<bool> UpdateUserActivity(string userId)
        {
            if (userId != null)
            {
                UserActivityDTO activityDTO = new UserActivityDTO();
                activityDTO.UserId = int.Parse(userId);
                activityDTO.OnlineOfflineStatus = SD.LocalStorage_ChatStatusOffline;
                activityDTO.LogoutDate = DateTime.Now;

                var response = await _httpClient.PutAsJsonAsync("Activity/updateuseractivity/", activityDTO);

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<UserActivityDTO>> GetStatus()
        {
          
            try
            {
                var response = await _httpClient.GetAsync("Activity/getuserstatuses/");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var userActivities = JsonConvert.DeserializeObject<IEnumerable<UserActivityDTO>>(content);

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
