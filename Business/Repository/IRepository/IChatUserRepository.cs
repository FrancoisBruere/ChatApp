using ChatApp.Shared;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IChatUserRepository
    {

        public Task<UserDTO> SignUp(UserDTO userDTO);
        public Task<IEnumerable<UserDTO>> GetAllUserProfiles();
        public Task<UserDTO> GetUserProfile(int userId);
        public Task<UserDTO> GetUserProfileByEmail(string email);
        public Task<UserDTO> UpdateUserProfile(int userId, UserDTO userDTO);
        public Task<int> DeleteUserProfile(int userId);
        public Task<UserDTO> GetUserForLogin(string email, string password);
        public Task<Message> AddChatHistory(Message historyDTO);
        public Task<UserContactsDTO> AddContact(UserContactsDTO contacts);
        public Task<IEnumerable<UserContactsDTO>> GetContacts(string email);
        
        public Task<bool> AddUserActivity(UserActivityDTO userActivity);

        public Task UpateUserActivity(UserActivityDTO updateUserActivity);

        public Task<IEnumerable<UserActivityDTO>> GetUserStatus();

        public Task<IEnumerable<ChatHistoryDTO>> GetUserChatHistoryById(int toId, int fromId);

        
    }
}
