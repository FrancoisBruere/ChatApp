using AutoMapper;
using Business.Repository.IRepository;
using ChatApp.Client.Pages;
using ChatApp.Shared;
using Common;
using DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {


        private readonly IChatUserRepository _chatUserRepository;
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;


        public UserController(IChatUserRepository chatUserRepository, ILogger<UserController> logger, IConfiguration configuration, ApplicationDbContext db, IHttpClientFactory httpClientFactory, IMapper mapper)
        {

            _chatUserRepository = chatUserRepository;
            _logger = logger;
            _configuration = configuration;
            _db = db;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
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


        #region - OldHttpCookie Login/GetcurrentUser
        //[HttpPost("loginuser")]
        //public async Task<ActionResult<UserDTO>>LoginUser(UserDTO userDTO)
        //{
        //    userDTO.Password = Utility.Encrypt(userDTO.Password);
        //    UserDTO loggedInUser = await _chatUserRepository.GetUserForLogin(userDTO.Email, userDTO.Password);

        //    if (loggedInUser != null)
        //    {
        //        // create a claim
        //        var claim = new Claim(ClaimTypes.Email, loggedInUser.Email);
        //        // claim id used for signalR
        //        var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, loggedInUser.UserId.ToString());


        //        // create claimsIdentity
        //        var claimsIdentity = new ClaimsIdentity(new[] { claim, claimNameIdentifier }, "serverAuth");
        //        // create claimsPrincipal
        //        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


        //        // sign user in
        //        await HttpContext.SignInAsync(claimsPrincipal, GetAuthenticationProperties()); // pass auth propperties here



        //    }
        //    else
        //    {
        //        return NotFound(userDTO);
        //    }


        //    return await Task.FromResult(loggedInUser);

        //}

        //[HttpGet("getcurrentuser")]
        //public async Task<ActionResult<UserDTO>> GetCurrentUser()
        //{
        //    UserDTO currentUser = new UserDTO();


        //    if (User.Identity.IsAuthenticated)
        //    {
        //        currentUser.Email = User.FindFirstValue(ClaimTypes.Email); //get state of user
        //        currentUser = await _chatUserRepository.GetUserProfileByEmail(currentUser.Email); //XXXXXXXXXXXXXX

        //        if (currentUser == null) // this is added for 3rd party login accounts
        //        {

        //            currentUser = new UserDTO();

        //            //currentUser.UserId = _db.Users.Max(User => User.UserId) + 1;


        //            currentUser.Email = User.FindFirstValue(ClaimTypes.Email);
        //            currentUser.Password = Utility.Encrypt(currentUser.Email);
        //            currentUser.Source = User.Identity.AuthenticationType; //"ExternalAccount";


        //            await _chatUserRepository.SignUp(currentUser);

        //        }


        //       // moved to JWT Auth below await _chatUserRepository.AddUserActivity(currentUser.UserId);
        //    }

        //    return await Task.FromResult(currentUser);

        //}
        #endregion

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
        public async Task<string> LogOutUser()
        {

            await HttpContext.SignOutAsync();

            return "Success";
        }

        [HttpGet("GoogleSignIn")]

        public async Task GoogleSignIn()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
            GetAuthenticationProperties());

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

       
        [HttpGet("getfacebookappId")]
        public ActionResult<string> GetFacebookAppId()
        {

            return _configuration["Authentication:Facebook:AppId"];

        }

       
        [HttpPost("getfacebookjwt")]
        public async Task<ActionResult<AuthenticationResponse>> GetFacebookJWT([FromBody] FacebookAuth facebookAuthRequest)
        {
            // 1.create a token and an http client
            string token = string.Empty;
            var client = _httpClientFactory.CreateClient();

            // 2.get AppId and AppSecrete
            string appId = _configuration["Authentication:Facebook:AppId"];
            string appSecrete = _configuration["Authentication:Facebook:AppSecret"];
            Console.WriteLine("\nApp Id : " + appId);
            Console.WriteLine("Secrete Id : " + appSecrete + "\n");

            // 3. generate an app access token
            var appAccessRequest = $"https://graph.facebook.com/oauth/access_token?client_id={appId}&client_secret={appSecrete}&grant_type=client_credentials";
            var appAccessTokenResponse = await client.GetFromJsonAsync<FacebookAppAccessToken>(appAccessRequest);
          

            // 4. validate the user access token
            var userAccessValidationRequest = $"https://graph.facebook.com/debug_token?input_token={facebookAuthRequest.AccessToken}&access_token={appAccessTokenResponse.Access_Token}";
            var userAccessTokenValidationResponse = await client.GetFromJsonAsync<FacebookUserAccessTokenValidation>(userAccessValidationRequest);
           

            if (!userAccessTokenValidationResponse.Data.Is_Valid)
                return BadRequest();

            // 5. we've got a valid token so we can request user data from facebook
            var userDataRequest = $"https://graph.facebook.com/v11.0/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={facebookAuthRequest.AccessToken}";
            var facebookUserData = await client.GetFromJsonAsync<FacebookUserData>(userDataRequest);
            

            //6. try to find the user in the database or create a new account
            var loggedInUser = await _db.Users.Where(user => user.Email == facebookUserData.Email).FirstOrDefaultAsync();

            UserDTO user = new UserDTO();

            //7. generate the token
            if (loggedInUser == null)
            {

                user.Email = facebookUserData.Email;
                user.Password = Utility.Encrypt(facebookUserData.Email);
                user.Source = SD.External_Facebook;

                var addedUser = await _chatUserRepository.SignUp(user);

                loggedInUser = _mapper.Map<UserDTO, User>(addedUser);
            
            }

            token = GenerateJwtToken(loggedInUser);
            
            return await Task.FromResult(new AuthenticationResponse() { Token = token });
        }


       

        // get authentication properties as per loginuser for social login accouts -  cookie settings "loginuser"

        public AuthenticationProperties GetAuthenticationProperties()
        {
            //authentication properties for cookie

            return new AuthenticationProperties()
            {
                               
                // cookie remains until user log out
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

        //Migrating to JWT Authorization...
        private string GenerateJwtToken(User user)
        {
            //getting the secret key
            string secretKey = _configuration["JWTSettings:SecretKey"];
            var key = Encoding.ASCII.GetBytes(secretKey);

            //create claims
            var claimEmail = new Claim(ClaimTypes.Email, user.Email);
            var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString());

            //create claimsIdentity
            var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier }, "serverAuth");

            // generate token that is valid for 1 days
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //creating a token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //returning the token back
            return tokenHandler.WriteToken(token);
        }

        
        [HttpPost("authenticatejwt")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateJWT(AuthenticationRequest authenticationRequest)
        {
            string token = string.Empty;

            //checking if the user exists in the database
            authenticationRequest.Password = Utility.Encrypt(authenticationRequest.Password);
            User loggedInUser = await _db.Users
                        .Where(u => u.Email == authenticationRequest.Email && u.Password == authenticationRequest.Password)
                        .FirstOrDefaultAsync();

            if (loggedInUser != null)
            {
                //generating the token
                token = GenerateJwtToken(loggedInUser);

            }
            return await Task.FromResult(new AuthenticationResponse() { Token = token });
        }

        
        [HttpPost("getuserbyjwt")]
        public async Task<ActionResult<User>> GetUserByJWT([FromBody] string jwtToken)
        {
            try
            {
                //getting the secret key
                string secretKey = _configuration["JWTSettings:SecretKey"];
                var key = Encoding.ASCII.GetBytes(secretKey);

                //preparing the validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                //validating the token
                var principle = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = (JwtSecurityToken)securityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    //returning the user if found
                    var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    
                    var result = await _db.Users.Where(u => u.UserId == Convert.ToInt64(userId)).FirstOrDefaultAsync();
                   
                    return result;
                }
            }
            catch (Exception ex)
            {
                //logging the error and returning null
                Console.WriteLine("Exception : " + ex.Message);
                return null;
            }
            //returning null if token is not validated
            return null;
        }

    }
}
