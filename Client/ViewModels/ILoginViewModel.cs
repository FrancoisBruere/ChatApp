using ChatApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    
        public interface ILoginViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }

            public Task<string> LoginUser();
            public Task<AuthenticationResponse> AuthenticateJWT();
        }

    

}
