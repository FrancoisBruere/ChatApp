using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public class ChatHistoryViewModel : IChatHistoryViewModel
    {
     
        private HttpClient _httpClient;


        public ChatHistoryViewModel()
        {

        }

        public ChatHistoryViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ChatHistoryDTO>> GetChatHistory(int fromId, int toId)
        {
            try
            {
                var response = await _httpClient.GetAsync("ChatHistory/getChatHistory/" + fromId + "/" + toId);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var histories = JsonConvert.DeserializeObject<IEnumerable<ChatHistoryDTO>>(content);

                    return histories;
                }

                return null;
               
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            


        }



    }
}
