using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public class SignUpViewModel : ISignUpViewModel
    {

        public int UserId { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [MinLength(6,ErrorMessage = "Password must be more than 6 characters long")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password is not matched")]
        public string ConfirmPassword { get; set; }

        private HttpClient _httpClient;

        public SignUpViewModel()
        {

        }

        public SignUpViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string> SignUpUser()
        {
            try
            {

                UserDTO user = this;

                if (user != null)
                {
                    var responce  = await _httpClient.PostAsJsonAsync("User/signup", user);
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

        private void LoadCurrentObject(SignUpViewModel signUpViewModel)
        {

            this.FirstName = signUpViewModel.FirstName;
            this.LastName = signUpViewModel.LastName;
            this.Email = signUpViewModel.Email;
            this.Password = signUpViewModel.Password;
            this.UserId = signUpViewModel.UserId;


        }


        public static implicit operator SignUpViewModel(UserDTO user)
        {
            return new SignUpViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                UserId = user.UserId,
            };   


        }

        public static implicit operator UserDTO(SignUpViewModel signUpViewModel)
        {

            return new UserDTO
            {
                FirstName = signUpViewModel.FirstName,
                LastName = signUpViewModel.LastName,
                Email = signUpViewModel.Email,
                Password = signUpViewModel.Password,
                UserId = signUpViewModel.UserId,


            };

        }
    }
}
