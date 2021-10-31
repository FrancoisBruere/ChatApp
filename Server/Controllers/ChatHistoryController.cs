using Business.Repository.IRepository;
using ChatApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]


    public class ChatHistoryController : Controller
    {
        private readonly IChatUserRepository _chatUserRepository;
        private readonly ILogger<ChatHistoryController> _logger;

        public ChatHistoryController(IChatUserRepository chatUserRepository, ILogger<ChatHistoryController> logger)
        {
            _chatUserRepository = chatUserRepository;
            _logger = logger;
        }

        [HttpPost("addChatHistory/")]
        public async Task<Message> AddChatHistory(Message chatHistory)
        {
            if (chatHistory != null)
            {
                await _chatUserRepository.AddChatHistory(chatHistory);

                
                return chatHistory;
            }

            throw new NullReferenceException();
        }


        [HttpGet("getChatHistory/{fromId}/{toId}")]  //added

        public async Task<IActionResult> GetChatHistory(int fromId, int toId)
        {
            if (fromId != 0 && toId != 0)
            {
                
                var userHistory = await _chatUserRepository.GetUserChatHistoryById(fromId, toId);
                return Ok(userHistory);
            }
            else
            {
                return null;
            }

            throw new NullReferenceException();
        }
    }
}
