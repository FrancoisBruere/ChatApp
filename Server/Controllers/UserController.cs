using Business.Repository.IRepository;
using ChatApp.Shared;
using DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

       
        private readonly IChatUserRepository _chatUserRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IChatUserRepository chatUserRepository, ILogger<UserController> logger)
        {
            
            _chatUserRepository = chatUserRepository;
            _logger = logger;
            
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserDTO>> SignUp([FromBody] UserDTO user)
        {
            user.Password = Utility.Encrypt(user.Password);
            var newUser = await _chatUserRepository.SignUp(user);

            if (newUser.UserId == 0)
            {
                return Ok(newUser.Email);
            }

            return Ok(newUser);

        }



        [HttpPost("loginuser")]
        public async Task<ActionResult<UserDTO>>LoginUser(UserDTO userDTO)
        {
            userDTO.Password = Utility.Encrypt(userDTO.Password);
            UserDTO loggedInUser = await _chatUserRepository.GetUserForLogin(userDTO.Email, userDTO.Password);

            if (loggedInUser != null)
            {
                // create a claim
                var claim = new Claim(ClaimTypes.Email, loggedInUser.Email);
                // claim id used for signalR
                var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, loggedInUser.UserId.ToString());


                // create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claim, claimNameIdentifier }, "serverAuth");
                // create claimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


                // sign user in
                await HttpContext.SignInAsync(claimsPrincipal, GetAuthenticationProperties()); // pass auth propperties here
                

                
            }
            else
            {
                return NotFound(userDTO);
            }

            
            return await Task.FromResult(loggedInUser);

        }

        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            UserDTO currentUser = new UserDTO();


            if (User.Identity.IsAuthenticated)
            {
                currentUser.Email = User.FindFirstValue(ClaimTypes.Email); //get state of user
                currentUser = await _chatUserRepository.GetUserProfileByEmail(currentUser.Email); //XXXXXXXXXXXXXX

                if (currentUser == null) // this is added for 3rd party login accounts
                {

                    currentUser = new UserDTO();
                   
                    //currentUser.UserId = _db.Users.Max(User => User.UserId) + 1;
                    
                    
                    currentUser.Email = User.FindFirstValue(ClaimTypes.Email);
                    currentUser.Password = Utility.Encrypt(currentUser.Email);
                    currentUser.Source = User.Identity.AuthenticationType; //"ExternalAccount";
                    

                    await _chatUserRepository.SignUp(currentUser);

                }

                               
                await _chatUserRepository.AddUserActivity(currentUser.UserId);
            }

            return await Task.FromResult(currentUser);

        }

        [HttpGet("getchattouser")]
        public async Task<ActionResult<UserDTO>> GetChatToUser()
        {
            UserDTO currentUser = new UserDTO();


            if (User.Identity.IsAuthenticated)
            {
                currentUser.Email = User.FindFirstValue(ClaimTypes.Email);
                currentUser = await _chatUserRepository.GetUserProfileByEmail(currentUser.Email);

            }

            return await Task.FromResult(currentUser);

        }

        [HttpGet("logoutuser")]
        public async Task<ActionResult<string>> LogOutUser()
        {
            UserDTO currentUser = new UserDTO();


            if (User.Identity.IsAuthenticated)
            {
                currentUser.Email = User.FindFirstValue(ClaimTypes.Email);
                currentUser = await _chatUserRepository.GetUserProfileByEmail(currentUser.Email);

                if (currentUser!=null)
                {
                    await _chatUserRepository.UpateUserActivity(currentUser.UserId);
                }
                
                
                
            }

            await HttpContext.SignOutAsync();
            return "Success";

        }



       

        [HttpGet("TwitterSignIn")]

        public async Task TwitterSignIn()
        {

           await HttpContext.ChallengeAsync(TwitterDefaults.AuthenticationScheme,
               GetAuthenticationProperties());
            
        }

        [HttpGet("FacebookSignIn")]

        public async Task FacebookSignIn()
        {

            await HttpContext.ChallengeAsync(FacebookDefaults.AuthenticationScheme,
                 GetAuthenticationProperties());

        }

        [HttpGet("GoogleSignIn")]

        public async Task GoogleSignIn()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
            GetAuthenticationProperties());

        }

        

        // get authentication properties as per loginuser for social login accouts -  cookie settings "loginuser"

        public AuthenticationProperties GetAuthenticationProperties()
        {
            //authentication properties for cookie

            return new AuthenticationProperties()
            {
                               
                IsPersistent = true, // cookie remains until user log out
                ExpiresUtc = DateTime.Now.AddHours(1),
                RedirectUri = "/profile",
            };

        }

        [HttpGet("notauthorized")]

        public IActionResult NotAuthorized()
        {
            
            //return Redirect("login");
            return Unauthorized();


        }

    }
}
