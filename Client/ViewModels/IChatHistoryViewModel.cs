using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public interface IChatHistoryViewModel
    {

        public Task<IEnumerable<ChatHistoryDTO>> GetChatHistory(int fromId, int toId);
    }
}
