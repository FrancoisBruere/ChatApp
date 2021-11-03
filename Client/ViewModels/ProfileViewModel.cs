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
    public class ProfileViewModel : IProfileViewModel
    {

        public int UserId { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string About { get; set; }
        public string ProfilePicUrl { get; set; }

        private HttpClient _httpClient;
        
        

        public ProfileViewModel()
        {

        }


        public ProfileViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task UpdateProfile()
        {

            UserDTO user = this;
            
            await _httpClient.PutAsJsonAsync("Profile/update/" + this.UserId, user);

        }

       

        public async Task<System.Net.HttpStatusCode> GetProfile()
        {
           
                UserDTO user = await _httpClient.GetFromJsonAsync<UserDTO>("Profile/getprofile/" + this.Email); // changed UserID
                if (user != null) // changed
                {
                    LoadCurrentObject(user);
                }

                return System.Net.HttpStatusCode.Unauthorized;  //Remember on other pages
        }

        public async Task<UserDTO> ChatUser(int userId)
        {
            UserDTO chatUser = await _httpClient.GetFromJsonAsync<UserDTO>("Profile/getprofileById/" + userId);
            if (chatUser != null)
            {
                return chatUser;
            }

            return null; 
        }

        public async Task<bool> DeleteProfile(int userId)
        {
            var response = await _httpClient.DeleteAsync("Profile/delete/" + userId);

            return response.IsSuccessStatusCode;
        }


        private void LoadCurrentObject(ProfileViewModel profileViewModel)
        {
            this.FirstName = profileViewModel.FirstName;
            this.LastName = profileViewModel.LastName;
            this.Email = profileViewModel.Email;
            this.About = profileViewModel.About;
            this.UserId = profileViewModel.UserId;
            this.ProfilePicUrl = profileViewModel.ProfilePicUrl;

        }

      

        public static implicit operator ProfileViewModel(UserDTO user)
        {

            return new ProfileViewModel
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                About = user.About,
                UserId = user.UserId,
                ProfilePicUrl = user.ProfilePicUrl
                
            };

        }

        public static implicit operator UserDTO(ProfileViewModel profileViewModel)
        {

            return new UserDTO
            {

                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                Email = profileViewModel.Email,
                About = profileViewModel.About,
                UserId = profileViewModel.UserId,
                ProfilePicUrl = profileViewModel.ProfilePicUrl
                
            };

        }

    }
}
