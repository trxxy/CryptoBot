using AutoMapper;
using DataAccess.Entities;
using Logic.DataTableObjects;
using System.Collections.Generic;

namespace Logic.Configuration.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<SushiDTO, Sushi>();

            CreateMap<KeyValuePair<SushiDTO, int>, SushiInBasket>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Sushi, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Value));

            CreateMap<Dictionary<SushiDTO, int>, List<SushiInBasket>>()
                .ForMember(dest => dest.Capacity, opt => opt.Ignore());

            CreateMap<BasketDTO, Basket>()
                .ForMember(dest => dest.SushiInBasketId, opt => opt.MapFrom(src => src.SushiId))
                .ReverseMap();
        }
    }
}