using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public interface ISignUpViewModel
    {
        public int UserId { get; set; }


        public string FirstName { get; set; }



        public string LastName { get; set; }



        public string Email { get; set; }


        public string Password { get; set; }


        public string ConfirmPassword { get; set; }


        public Task<string> SignUpUser();
    }
}
