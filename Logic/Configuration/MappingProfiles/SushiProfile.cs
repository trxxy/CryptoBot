using System;
using AutoMapper;
using DataAccess.Entities;
using Logic.DataTableObjects;
using SushiHouseParser;

namespace Logic.Configuration.MappingProfiles
{
    public class SushiProfile : Profile
    { 
        public SushiProfile()
        {
            CreateMap<Sushi, SushiDTO>();
            CreateMap<ParsedSushi, SushiDTO>();
            CreateMap<ParsedSushi, Sushi>();
        }
    }
}