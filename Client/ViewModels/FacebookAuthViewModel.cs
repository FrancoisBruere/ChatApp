using ChatApp.Client.Pages;
using ChatApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public class FacebookAuthViewModel : IFacebookAuthViewModel
    {

        private readonly HttpClient _httpClient;
        public FacebookAuthViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetFacebookJWTAsync(string accessToken)
        {
            var httpMessageResponse = await _httpClient.PostAsJsonAsync<FacebookAuth>("User/getfacebookjwt", new FacebookAuth() { AccessToken = accessToken });
            return (await httpMessageResponse.Content.ReadFromJsonAsync<AuthenticationResponse>()).Token;
        }
    }
}
