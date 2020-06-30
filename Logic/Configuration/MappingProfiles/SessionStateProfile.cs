using AutoMapper;
using DataAccess.Entities;
using Logic.DataTableObjects;

namespace Logic.Configuration.MappingProfiles
{
    public class SessionStateProfile : Profile
    {
        public SessionStateProfile()
        {
            CreateMap<SessionStateDTO, SessionState>()
                .ForPath(dest => dest.Basket.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}