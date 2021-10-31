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


        public async Task<IEnumerable<UserActivityDTO>> GetStatus()
        {
          
            try
            {
                var response = await _httpClient.GetAsync("Activity/getuserstatuses");

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
