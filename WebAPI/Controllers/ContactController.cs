using Business.Repository.IRepository;
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

    public class ContactController : Controller
    {
        private readonly IChatUserRepository _chatUserRepository;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IChatUserRepository chatUserRepository, ILogger<ContactController> logger)
        {
            _chatUserRepository = chatUserRepository;
            _logger = logger;
        }

        [HttpPost("addContact")]
        public async Task<UserContactsDTO> AddContact(UserContactsDTO contacts)
        {
            if (contacts != null)
            {
                await _chatUserRepository.AddContact(contacts);
                

                return contacts;
            }

            throw new NullReferenceException();
        }

        [HttpGet("getContacts/{email}")]

        public async Task<IActionResult> GetContacts(string email)
        {
            if (email != null)
            {

                var myContacts = await _chatUserRepository.GetContacts(email);
                return Ok(myContacts);
            }
            else
            {
                return null;
            }

            throw new NullReferenceException();
        }

    

    }
}
