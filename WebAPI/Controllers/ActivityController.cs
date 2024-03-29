﻿using Business.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ChatApp.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class ActivityController : Controller
    {
        private readonly IChatUserRepository _chatUserRepository;
        private readonly ILogger<ActivityController> _logger;

        public ActivityController(IChatUserRepository chatUserRepository, ILogger<ActivityController> logger)
        {
            _chatUserRepository = chatUserRepository;
            _logger = logger;
        }

        [HttpGet("getuserstatuses")]
        public async Task<IActionResult> GetUserStatus()
        {

            var userStatuses = await _chatUserRepository.GetUserStatus();
            return Ok(userStatuses);

        }

        [HttpPost("adduseractivity")]
        public async Task<bool> AddUserActivity(UserActivityDTO userActivity)
        {
            await _chatUserRepository.AddUserActivity(userActivity);
            return true;

        }


        [HttpPut("updateuseractivity")]
        public async Task<bool> UpdateUserActivity(UserActivityDTO updateUserActivity)
        {
            await _chatUserRepository.UpateUserActivity(updateUserActivity);
            return true;

        }
    }
}
