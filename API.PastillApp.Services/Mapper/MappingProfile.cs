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
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email!.ToLower()));

            CreateMap<CreateUserDTO, User>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Lastname))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));


            CreateMap<CreateDailyStatusDTO, DailyStatus>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Observation, opt => opt.MapFrom(src => src.Observation))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Symptoms, opt => opt.MapFrom(src => src.Symptoms));


            CreateMap<CreateMedicineDTO, Medicine>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Dosage, opt => opt.MapFrom(src => src.Dosage));
                

            CreateMap<CreateReminderDTO, Reminder>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.MedicineId, opt => opt.MapFrom(src => src.MedicineId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.DateTimeStart, opt => opt.MapFrom(src => src.DateTimeStart))
                .ForMember(dest => dest.FrequencyText, opt => opt.MapFrom(src => src.FrequencyText))
                .ForMember(dest => dest.FrequencyNumber, opt => opt.MapFrom(src => src.FrequencyNumber))
                .ForMember(dest => dest.EmergencyAlert, opt => opt.MapFrom(src => src.EmergencyAlert))
                .ForMember(dest => dest.Observation, opt => opt.MapFrom(src => src.Observation))
                .ForMember(dest => dest.IntakeTimeNumber, opt => opt.MapFrom(src => src.IntakeTimeNumber))
                .ForMember(dest => dest.IntakeTimeText, opt => opt.MapFrom(src => src.IntakeTimeText))
                .ForMember(dest => dest.Presentation, opt => opt.MapFrom(src => src.Presentation))
                .ForMember(dest => dest.ReminderDateTimes, opt => opt.MapFrom(src => src.ReminderDateTimes))
                .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime));
           


            CreateMap<UpdateUserDTO, User>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => !string.IsNullOrWhiteSpace(src.Name)))
                .ForMember(dest => dest.LastName, opt => opt.Condition(src => !string.IsNullOrWhiteSpace(src.LastName)))
                .ForMember(dest => dest.Email, opt => opt.Condition(src => !string.IsNullOrWhiteSpace(src.Email)));

            CreateMap<UpdateDailyStatusDTO, DailyStatus>()
                .ForMember(dest => dest.Symptoms, opt => opt.MapFrom(src => src.Symptoms))
                .ForMember(dest => dest.Observation, opt => opt.MapFrom(src => src.Observation));

        }
    }
}
