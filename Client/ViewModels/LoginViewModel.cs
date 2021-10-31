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
