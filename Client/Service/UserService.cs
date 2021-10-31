using ChatApp.Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatApp.Client.Service
{
    public class UserService : IUserService
    {

        //private readonly HttpClient _client;
        //public UserService(HttpClient client)
        //{
        //    _client = client;
        //}


        //public async Task<IEnumerable<UserDTO>> GetAllUserProfiles()
        //{
        //    var responce = await _client.GetAsync($"User/");
        //    var content = await responce.Content.ReadAsStringAsync();
        //    var usersprofiles = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(content);

        //    return usersprofiles;
        //}

        //public async Task <UserDTO> GetUserProfile(int userId)
        //{
        //    var responce = await _client.GetAsync($"User/{userId}");

            

        //    if (responce.IsSuccessStatusCode)
        //    {
        //        var content = await responce.Content.ReadAsStringAsync();
               
        //        var userprofile = JsonConvert.DeserializeObject<UserDTO>(content);

        //        return userprofile;
                
        //    }
        //    else
        //    {
                
        //        throw new Exception();
        //    }

        //}

    
    }
}
