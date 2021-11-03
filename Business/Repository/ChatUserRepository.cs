using AutoMapper;
using Business.Repository.IRepository;
using ChatApp.Shared;
using Common;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Business.Repository
{
   
    public class ChatUserRepository : IChatUserRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        
        

        public ChatUserRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            
        }


        public async Task<UserDTO> SignUp(UserDTO userDTO)
        {

            
            User user = _mapper.Map<UserDTO, User>(userDTO);
            var emailFound = await _db.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            
            
            if (emailFound is null)
            {
               

                if (user.Source == SD.External_Google || user.Source == SD.External_Facebook || user.Source == SD.External_Twitter)
                {
                    user.FirstName = SD.Internal_UserName;
                    user.LastName = SD.Internal_UserLastName;
                    user.CreatedDate = DateTime.Now;
                    user.UpdateDate = DateTime.Now;
                    user.DarkTheme = 0;
                    user.Notifications = 1;
                    user.Email = user.Email.ToLower();
                    
                }
                else
                {
                    user.CreatedDate = DateTime.Now;
                    user.UpdateDate = DateTime.Now;
                    user.Source = SD.Internal_Registration;
                    user.DarkTheme = 0;
                    user.Notifications = 1;
                    user.Email = user.Email.ToLower();
                }

                

                try
                {
                    var addedUser = await _db.Users.AddAsync(user);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<User, UserDTO>(addedUser.Entity);
                }
                catch (Exception e)
                {
                    string innermessage = (e.InnerException.Message);
                    throw new Exception(e.Message);

                }
                
            }
            else
            {

                return new UserDTO()
                {
                    Email = user.Email,
                    
                };


            }

            

        }

        public async Task<Message> AddChatHistory(Message historyDTO)
        {
            

            if (historyDTO != null)
            {
                ChatHistory currentRecord = new ChatHistory();
                currentRecord.ToUserId = int.Parse(historyDTO.ToUserId);
                currentRecord.FromUserId = int.Parse(historyDTO.FromUserId);
                currentRecord.Message = historyDTO.MessageText;
                currentRecord.CreatedDate = historyDTO.CreatedDate;
                currentRecord.ConversationId = historyDTO.FromUserId + "," + historyDTO.ToUserId;
                
                await _db.ChatHistories.AddAsync(currentRecord);
                
                await _db.SaveChangesAsync();

                return null;
            }

            throw new DbUpdateException();

        }

        public async Task<IEnumerable<UserDTO>> GetAllUserProfiles()
        {
            IEnumerable<UserDTO> usersDTO =
                _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_db.Users);
            
            return usersDTO;
        }

        public async Task<UserDTO> GetUserProfile(int userId)
        {
            UserDTO user = _mapper.Map<User, UserDTO>(
                await _db.Users.FirstOrDefaultAsync(x => x.UserId == userId));

            return user;
        }

        public async Task<UserDTO> GetUserProfileByEmail(string email)
        {
            if (email != null)
            {
                UserDTO user = _mapper.Map<User, UserDTO>(
                await _db.Users.FirstOrDefaultAsync(x => x.Email == email));

             
                return user;
            }
            else
            {
                throw new ArgumentNullException();
            }

            
        }

        public async Task<IEnumerable<UserActivityDTO>> GetUserStatus()
        {
            try
            {
                IEnumerable<UserActivityDTO> userActivityDTOs =
              _mapper.Map<IEnumerable<UserActivity>, IEnumerable<UserActivityDTO>>(_db.UserActivities.Where(x => x.LogoutDate == null));


                return userActivityDTOs;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
           
        }

        public async Task<bool> AddUserActivity(UserActivityDTO submitActivity) //LOOK AT LOGIC AGAIN
        {
            
            if (submitActivity.UserId != 0)
            {
                UserDTO user = _mapper.Map<User, UserDTO>(
                await _db.Users.FirstOrDefaultAsync(x => x.UserId == submitActivity.UserId));

                UserActivityDTO userActivityDTO = _mapper.Map<UserActivity, UserActivityDTO>(
                    await _db.UserActivities.OrderBy(x => x.LogoutDate).FirstOrDefaultAsync(x => x.UserId == submitActivity.UserId));

                if (userActivityDTO == null || userActivityDTO.LogoutDate != null)
                {
                    UserActivity userActivity = new UserActivity();
                    userActivity.UserId = user.UserId;
                    userActivity.LoginDate = DateTime.Now;
                    userActivity.OnlineOfflineStatus = SD.LocalStorage_ChatStatusOnline;

                    await _db.UserActivities.AddAsync(userActivity);

                    await _db.SaveChangesAsync();

                    return true;
                }

            }
            return false;
        }

        public async Task UpateUserActivity(UserActivityDTO updateUserActivity)
        {
            

            if (updateUserActivity.UserId != 0)
            {
                
                UserActivity userActivity = await _db.UserActivities.OrderBy(x=>x.LoginDate).LastOrDefaultAsync(x=>x.UserId == updateUserActivity.UserId && x.LogoutDate == null);

                userActivity.LogoutDate = DateTime.Now;
                userActivity.OnlineOfflineStatus = SD.LocalStorage_ChatStatusOffline;
                await _db.SaveChangesAsync();
            }
           
        }

        public async Task<UserDTO> GetUserForLogin(string email, string password)
        {

            try
            {
                UserDTO user = _mapper.Map<User, UserDTO> (
                await _db.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password));

                return user;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

          

        }
        public async Task<UserDTO> UpdateUserProfile(int userId, UserDTO userDTO)
        {

            if (userId == userDTO.UserId)
            {
                User userProfileDetails = await _db.Users.FindAsync(userId);

                userProfileDetails.FirstName = userDTO.FirstName;
                userProfileDetails.LastName = userDTO.LastName;
                userProfileDetails.About = userDTO.About;
                userProfileDetails.Email = userDTO.Email;
                userProfileDetails.UpdateDate = DateTime.Now;
                userProfileDetails.ProfilePicUrl = userDTO.ProfilePicUrl;
                userProfileDetails.UserId = userDTO.UserId;
               
                await _db.SaveChangesAsync();

                return null;
                

            }
            else
            {

                return null;

            }
            
        }

        public async Task<int> DeleteUserProfile(int userId)
        {
            if (userId !=0)
            {
                var userProfileDetails = await _db.Users.FindAsync(userId);
                var userContacts =  _db.UserContacts.Where(x=>x.ContactBelongId == userId);
                var userHistory = _db.ChatHistories.Where(x => x.FromUserId == userId);
                

                try
                {
                    if (userProfileDetails != null)
                    {
                        _db.Users.Remove(userProfileDetails);
                        _db.UserContacts.RemoveRange(userContacts);
                        _db.ChatHistories.RemoveRange(userHistory);
                        

                        var result =  await _db.SaveChangesAsync();
                        return result;
                    }
                    else
                    {
                        throw new DbUpdateException("Unable to remove requested profile");
                    }
                }
                catch (Exception e)
                {
                    string innermessage = (e.InnerException.Message);
                    throw new Exception(e.Message);

                }

                

            }
            else
            {

                return 0;

            }
        }

        public async Task<IEnumerable<ChatHistoryDTO>> GetUserChatHistoryById(int toId, int fromId)
        {
            var conversationFromId = fromId.ToString() + "," + toId.ToString(); //here
            var conversationToId = toId.ToString() + "," + fromId.ToString();

            IEnumerable<ChatHistoryDTO> chatHistoryDTOs =
                _mapper.Map<IEnumerable<ChatHistory>,
                IEnumerable<ChatHistoryDTO>>(_db.ChatHistories.Where(x=>x.ConversationId == conversationFromId || x.ConversationId == conversationToId));

            return chatHistoryDTOs;

            //ChatHistoryDTO chatHistory = _mapper.Map<ChatHistory, ChatHistoryDTO>(await _db.ChatHistories.FirstOrDefaultAsync(x => x.FromUserId == fromId && x.ToUserId == toId));
            //return chatHistory;


        }

        public async Task<UserContactsDTO> AddContact(UserContactsDTO contacts)
        {

            if (contacts != null)
            {
                UserContactsDTO userContactsDTO = _mapper.Map<UserContact, UserContactsDTO>(
                    await _db.UserContacts.FirstOrDefaultAsync(x => x.UserId == contacts.UserId && x.ContactBelongId == contacts.ContactBelongId));

                if (userContactsDTO is null)
                {
                    UserContact userContact = new UserContact();
                    userContact.FirstName = contacts.FirstName;
                    userContact.LastName = contacts.LastName;
                    userContact.Email = contacts.Email;
                    userContact.UserId = contacts.UserId;
                    userContact.ContactBelongId = contacts.ContactBelongId;


                    await _db.UserContacts.AddAsync(userContact);

                    await _db.SaveChangesAsync();

                    return _mapper.Map<UserContact, UserContactsDTO>(userContact);

                }

                return null;

               
            }

            throw new DbUpdateException();
        }

        public async Task<IEnumerable<UserContactsDTO>> GetContacts(string email)
        {
            UserDTO user = _mapper.Map<User, UserDTO>(
               await _db.Users.FirstOrDefaultAsync(x => x.Email == email));

            IEnumerable<UserContactsDTO> userContacts =
                _mapper.Map<IEnumerable<UserContact>,
                IEnumerable<UserContactsDTO>>(_db.UserContacts.Where(x => x.ContactBelongId == user.UserId));

           

            return userContacts;
        }

       
    }
}
