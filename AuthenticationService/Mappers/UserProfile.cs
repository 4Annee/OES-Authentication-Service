using AuthenticationService.DTOs.AppUser;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUserDtoForCreation, UserModel>()
                .ForMember(e=>e.UserName,opt=>opt.MapFrom(s=>s.Email)).ReverseMap();
            CreateMap<AppUserDtoForLogin, UserModel>().ReverseMap();
            CreateMap<UserModel, AppUserDto>().ReverseMap();
            CreateMap<AppUserDtoForChangingPassword, UserModel>().ReverseMap();
        }
    }
}
