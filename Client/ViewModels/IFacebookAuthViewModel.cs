using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public interface IFacebookAuthViewModel
    {
      
            public Task<string> GetFacebookJWTAsync(string accessToken);
      

    }
}
