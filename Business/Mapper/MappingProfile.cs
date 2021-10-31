using AutoMapper;
using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<ChatHistoryDTO, ChatHistory>().ReverseMap();
            CreateMap<UserActivityDTO, UserActivity>().ReverseMap();
            CreateMap<UserSearchDTO, User>().ReverseMap();
            CreateMap<UserContactsDTO, UserContact>().ReverseMap();
        }

    }
}
