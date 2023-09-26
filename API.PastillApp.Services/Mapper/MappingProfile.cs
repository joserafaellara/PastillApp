using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));


            CreateMap<CreateDailyStatusDTO, DailyStatus>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Observation, opt => opt.MapFrom(src => src.Observation))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Symptoms, opt => opt.MapFrom(src => src.Symptoms));

        }
    }
}
