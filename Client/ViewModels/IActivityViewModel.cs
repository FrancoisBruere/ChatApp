using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public interface IActivityViewModel
    {


        public Task<IEnumerable<UserActivityDTO>> GetStatus();

    }
}
