using AutoMapper;
using KarmaBook.Models;
using KarmaBook.Models.DTOs;

namespace KarmaBook.Extensions
{
    public class ObjectDtoMapper:Profile
    {
        public ObjectDtoMapper()
        {
            CreateMap<User, UserRegisterDto>();           
            CreateMap<UserRegisterDto,User>();

            /* 
             * Custom Property Matching
             * 
             * CreateMap<UserRegisterDto, User>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.FirstName));
             *
             */

        }
    }
}
