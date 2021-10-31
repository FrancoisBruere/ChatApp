using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public interface IProfileViewModel
    {

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
        public string ProfilePicUrl { get; set; }



        public Task UpdateProfile();
     
        public Task<System.Net.HttpStatusCode> GetProfile();

        //public Task<string> DeleteProfile();


    }
}
