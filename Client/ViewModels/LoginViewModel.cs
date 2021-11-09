using ChatApp.Shared;
using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        private readonly HttpClient _httpClient;

        public LoginViewModel()
        {

        }
        public LoginViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginUser()
        {
          var httpResponse =  await _httpClient.PostAsJsonAsync<UserDTO>("User/loginuser", this);

            if (httpResponse.IsSuccessStatusCode)
            {
                return httpResponse.StatusCode.ToString();
            }


            return httpResponse.StatusCode.ToString();

        }


        // JWT To call for razor component -  login

        public async Task<AuthenticationResponse> AuthenticateJWT()
        {
            // create Auth Request
            AuthenticationRequest authenticationRequest = new AuthenticationRequest();
            authenticationRequest.Email = this.Email;
            authenticationRequest.Password = this.Password;

            // Authenticate the req
            var httpMessageResponse = await _httpClient.PostAsJsonAsync<AuthenticationRequest>($"User/authenticatejwt", authenticationRequest);

            //send token to client to store
            return await httpMessageResponse.Content.ReadFromJsonAsync<AuthenticationResponse>();


        }

        
        public async Task<string> GetFacebookIdAsync()
        {
            return await _httpClient.GetStringAsync("User/getfacebookappId");
        }



        public static implicit operator LoginViewModel(UserDTO user)
        {
            return new LoginViewModel
            {
                Email = user.Email,
                Password = user.Password
            };
        }

        public static implicit operator UserDTO(LoginViewModel loginViewModel)
        {
            return new UserDTO
            {
                Email = loginViewModel.Email,
                Password = loginViewModel.Password
            };
        }


    }
}
