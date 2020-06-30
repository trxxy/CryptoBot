using AutoMapper;
using DataAccess.Entities;
using Logic.DataTableObjects;

namespace Logic.Configuration.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}