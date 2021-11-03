using Business.Repository.IRepository;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IChatUserRepository _chatUserRepository;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IChatUserRepository chatUserRepository, ILogger<ProfileController> logger)
        {
            _chatUserRepository = chatUserRepository;
            _logger = logger;
        }


        [HttpGet("getallprofiles")]
        public async Task<IActionResult> GetAllUserProfiles()
        {

            var allUsers = await _chatUserRepository.GetAllUserProfiles();
            return Ok(allUsers);
            
        }


        [HttpGet("getprofileById/{userId}")]

        public async Task<UserDTO> GetUserProfileId(int userId)
        {
            if (userId != 0)
            {
                var user = await _chatUserRepository.GetUserProfile(userId);
                return user;
            }

            throw new NullReferenceException();

        }

        [HttpGet("getprofile/{email}")]  //added

        public async Task<IActionResult> GetUserProfile(string email)
        {
            if (email!=null)
            {
                var user = await _chatUserRepository.GetUserProfileByEmail(email);

                return Ok(user);
            }

            throw new NullReferenceException();
        }


        [HttpPut("update/{userId}")]
        public async Task<UserDTO> UpdateProfile(int userId, [FromBody] UserDTO user)
        {
            if (userId != 0 && user !=null)
            {
                var updatedProfile = await _chatUserRepository.UpdateUserProfile(userId, user);

                return updatedProfile;
            }

            throw new NullReferenceException();
        }

        [HttpDelete ("delete/{userId}")]
        public async Task<int> DeleteProfile(int userId)
        {
            if (userId != 0)
            {
                return await _chatUserRepository.DeleteUserProfile(userId);
            }

            throw new NullReferenceException();


        }


        [HttpGet("DownloadServerFile")]

        public async Task<string> DownloadServerFile()
        {

            var filePath = @"Z:\ServerDownloadFile\TestingFile.txt";

            using (var fileInput = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {

                MemoryStream memoryStream = new MemoryStream();
                await fileInput.CopyToAsync(memoryStream);

                var buffer = memoryStream.ToArray();

                return Convert.ToBase64String(buffer);


            }


        }
    }
}
